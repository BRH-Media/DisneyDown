﻿using DisneyDown.Common.API;
using DisneyDown.Common.API.Schemas.ServicesSchema;
using DisneyDown.Common.Globals;
using DisneyDown.Common.Processors;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using DisneyDown.Common.KeySystem;

// ReSharper disable InvertIf
// ReSharper disable LocalizableElement
// ReSharper disable InconsistentNaming

namespace DisneyDown.Console
{
    /// <summary>
    /// DisneyDown.Console main program code
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Name of the the executable (used in-case the user renames it)
        /// </summary>
        public static string ExecutableName
            => Path.GetFileName(Assembly.GetEntryAssembly()?.Location);

        /// <summary>
        /// Launches default program to play the media file
        /// </summary>
        /// <param name="filePath"></param>
        private static void PlayContent(string filePath)
        {
            try
            {
                //validation
                if (File.Exists(filePath))
                {
                    //new process
                    var p = new Process
                    {
                        StartInfo =
                        {
                            FileName = filePath
                        }
                    };

                    //start it
                    p.Start();
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Playback failed; file does not exist.");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Content playback error: {ex.Message}");
            }
        }

        /// <summary>
        /// Returns the output file name specified. If nothing was specified or it was invalid, it will return the default value.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static string GetOutputFileName(IReadOnlyList<string> args)
        {
            try
            {
                //validation
                if (args.Count > 2)
                {
                    //grab the argument
                    var outFileNameArg = args[2];

                    //valid name (not an argument)
                    var validName = Args.Definitions.Any(n =>
                        string.Equals(n.Key, outFileNameArg, StringComparison.InvariantCultureIgnoreCase));

                    //valid characters (not an illegal file name on Windows)
                    var validPath = outFileNameArg.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;

                    //perform validation
                    return validName && validPath
                            ? Strings.DefaultOutFileName
                            : outFileNameArg;
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Output file name parse error: {ex.Message}");
            }

            //default
            return Strings.DefaultOutFileName;
        }

        /// <summary>
        /// Prints the 'help' text
        /// </summary>
        private static void PrintUsage()
        {
            try
            {
                //square-bracket list of all arguments
                var argumentList = Args.Definitions.Select(a => $"[{a.Key}]").Aggregate(@"", (current, argument) => current + $"{argument} ");

                //generate the argument square-bracket list

                //trim any trailing spaces
                argumentList = argumentList.TrimEnd(' ');

                //program name and argument summary
                System.Console.WriteLine(
                    $"{ExecutableName}\n\nmaster_manifest_URL widevine_hex_key [output_file_name] {argumentList}");
                System.Console.WriteLine("\n options:");

                //print hard-coded parameter definitions
                System.Console.WriteLine(
                    @"  master_manifest_URL - m3u8 master manifest URL; do not input a locally available manifest");
                System.Console.WriteLine(
                    @"  widevine_hex_key    - 32 character content decryption key");
                System.Console.WriteLine(
                    @"  output_file_name    - Name of the remuxed file in the .\output folder");

                //hard-coded line length of each argument name
                const int lineLength = 19;

                //go through each argument definition
                foreach (var a in Args.Definitions)
                {
                    //whitespace to append
                    var whitespace = new string(' ', lineLength - a.Key.Length);

                    //argument name line to prepend
                    var argumentLine = $@"{a.Key}{whitespace}";

                    //print the argument itself and its description
                    System.Console.WriteLine(
                        $@"  {argumentLine} - {a.Value}");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Print usage error: {ex.Message}");
            }
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            try
            {
                //execution timer
                Timers.StartTimer(Timers.Generic.ExecutionTimer);

                //verify arguments
                if (args.Length < 1
                    || args.Contains(@"-?")
                    || args.Contains(@"-help")
                    || args.Contains(@"-h"))

                    //help information
                    PrintUsage();
                else
                {
                    //make sure -a and -v are not used together
                    if (Args.AudioOnlyMode && Args.VideoOnlyMode)

                        //alert the user to the problem
                        System.Console.WriteLine(
                            "\n-a and -v flags cannot be combined; this would result in a null output.");
                    else
                    {
                        //get file from URL
                        var type = args[0].GetFileNameFromUrl();

                        //validation
                        if (!string.IsNullOrWhiteSpace(type))
                        {
                            //provision API services
                            Common.API.Globals.Objects.Services = ServiceInformation.GetServices();

                            //debugging
                            ConsoleWriters.DebugMode = Args.DebugModeEnabled;

                            //fallback filter?
                            if (Verification.SegmentFilter.FallbackTriggered)
                            {
                                //confirm with the user
                                ConsoleWriters.ConsoleWriteQuestion("One or more fallback filter strings were enabled; " +
                                    "fallback filter strings should only be used in special circumstances as they may cause problems with normal segment filtering. " +
                                    "Are you sure you would like to proceed? [y/n]");

                                //read response
                                var r = System.Console.ReadLine();

                                //verification
                                if (r.ToLower().Replace(@" ", "") == @"n")
                                {
                                    //exit
                                    return;
                                }
                            }

                            //get the type of file
                            var extension = Path.GetExtension(type);

                            //file-type validation
                            if (extension != @".m3u" && extension != @".m3u8")
                            {
                                //can we use the Disney+ API?
                                if (!Args.ApiDisabled)
                                {
                                    //inform user of what's going on
                                    ConsoleWriters.ConsoleWriteInfo(@"A valid manifest URL was not specified; trying to fetch from the Disney+ API");

                                    //attempt to get a manifest from the API
                                    var url = Common.API.Globals.Objects.Configuration.DeviceContext
                                        .RequestManifestFromUrl(args[0]);

                                    //validation
                                    if (!string.IsNullOrWhiteSpace(url?.ManifestUrl))
                                    {
                                        //override the provided URL with the new manifest URL
                                        args[0] = url.ManifestUrl;

                                        //attempt to get a new file name
                                        var fileName = url.CreateFileName();

                                        //validation
                                        if (!string.IsNullOrWhiteSpace(fileName))
                                        {
                                            //assign to global
                                            Strings.DefaultOutFileName = fileName;
                                        }
                                    }
                                    else
                                    {
                                        //die
                                        return;
                                    }
                                }
                                else
                                {
                                    //invalid URL
                                    ConsoleWriters.ConsoleWriteError(@"Invalid playlist URL");

                                    //die
                                    return;
                                }
                            }

                            //set required globals
                            Strings.ManifestUrl = args[0];
                            Strings.DecryptionKey = args.Length > 1
                                ? args[1]
                                : Strings.LookupModeTriggerKey;
                            Strings.OutFileName = GetOutputFileName(args);

                            //before we begin, check the validity of the decryption key
                            if (string.IsNullOrWhiteSpace(Strings.DecryptionKey)
                                || Strings.DecryptionKey?.Length != 32)
                            {
                                //report status
                                ConsoleWriters.ConsoleWriteInfo(@"No key provided; key lookup necessary");

                                //set the decryption key to the lookup trigger
                                Strings.DecryptionKey = Strings.LookupModeTriggerKey;
                            }

                            //begin
                            var outFile = MainProcessor.StartProcessor();

                            //validation
                            if (!string.IsNullOrWhiteSpace(outFile) && !Args.InputDisabled)
                            {
                                //report finality
                                ConsoleWriters.WriteLine("\nDone! Play now? (y/n)");

                                //read the console line
                                var response = System.Console.ReadLine();

                                //figure out the response
                                if (response == @"y")
                                    PlayContent(outFile);
                            }
                            else
                            {
                                //report finality
                                ConsoleWriters.WriteLine("\nDone!");
                            }

                            //main execution timer stop
                            Timers.StopTimer(Timers.Generic.ExecutionTimer);

                            //line break for timings
                            if (Timers.TimersEnabled)
                                System.Console.WriteLine();

                            //report all diagnostics timings
                            Timers.ReportTimers();
                        }
                        else
                        {
                            //help information
                            PrintUsage();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Main system error: {ex}");
            }
        }
    }
}
using DisneyDown.Common.Globals;
using DisneyDown.Common.Processors;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

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
        public static string ExecutableName => Path.GetFileName(Assembly.GetEntryAssembly()?.Location);

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
                    System.Console.Write(@"Playback failed; file does not exist.");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($@"Content playback error: {ex.Message}");
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

                    //validate all arguments
                    var invalidNames = new[]
                    {
                        @"-t",
                        @"-h",
                        @"-help",
                        @"-?",
                        @"-e",
                        @"-b",
                        @"-a",
                        @"-v",
                        @"-e"
                    };

                    //valid name (not an argument)
                    var validName = invalidNames.Any(n =>
                        string.Equals(n, outFileNameArg, StringComparison.InvariantCultureIgnoreCase));

                    //valid characters (not an illegal file name on Windows)
                    var validPath = outFileNameArg.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;

                    //perform validation
                    return validName && validPath
                            ? Strings.OutFileName
                            : outFileNameArg;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Output file name parse error: {ex.Message}");
            }

            //default
            return Strings.OutFileName;
        }

        /// <summary>
        /// Prints the 'help' text
        /// </summary>
        private static void PrintUsage()
        {
            System.Console.WriteLine(
                $"{ExecutableName} widevine_hex_key master_manifest_URL [output_file_name] [-t] [-a] [-v] [-e]");
            System.Console.WriteLine("\n options:");
            System.Console.WriteLine(@"  widevine_hex_key    - 32 character content decryption key");
            System.Console.WriteLine(
                @"  master_manifest_URL - m3u8 master manifest URL; do not input a locally available manifest");
            System.Console.WriteLine(@"  output_file_name    - Name of the remuxed file in the .\output folder");
            System.Console.WriteLine(
                @"  -t                  - Enables execution timing; reports how long each operation took");
            System.Console.WriteLine(
                @"  -a                  - Forces the application to only download and decrypt audio");
            System.Console.WriteLine(
                @"  -v                  - Forces the application to only download and decrypt video");
            System.Console.WriteLine(
                @"  -e                  - Forces the application to avoid using the master parser; requires -v or -a.");
        }

        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            //main execution timer start
            Timers.StartTimer(Timers.ExecutionTimer);

            //verify arguments
            if (args.Length < 2
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
                    System.Console.WriteLine("\n-a and -v flags cannot be combined; this would result in a null output.");
                else
                {
                    //set required globals
                    Strings.DecryptionKey = args[0];
                    Strings.ManifestUrl = args[1];
                    Strings.OutFileName = GetOutputFileName(args);

                    //begin
                    var outFile = MainProcessor.StartProcessor();

                    //validation
                    if (!string.IsNullOrWhiteSpace(outFile))
                    {
                        //report finality
                        System.Console.WriteLine("\nDone! Play now? (y/n)");

                        //read the console line
                        var response = System.Console.ReadLine();

                        //figure out the response
                        if (response == @"y")
                            PlayContent(outFile);
                    }

                    //main execution timer stop
                    Timers.StopTimer(Timers.ExecutionTimer);

                    //line break for timings
                    if (Timers.TimersEnabled)
                        System.Console.WriteLine();

                    //report all diagnostics timings
                    Timers.ReportTimers();
                }
            }
        }
    }
}
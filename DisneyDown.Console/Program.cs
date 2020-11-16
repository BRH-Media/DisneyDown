using DisneyDown.Common;
using System;
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
        /// Prints the 'help' text
        /// </summary>
        private static void PrintUsage()
        {
            System.Console.WriteLine($@"usage: {ExecutableName} widevine_hex_key master_manifest_URL [output_file_name] [-t]");
            System.Console.WriteLine(@" options:");
            System.Console.WriteLine(@"  widevine_hex_key    - 32 character content decryption key");
            System.Console.WriteLine(@"  master_manifest_URL - m3u8 master manifest URL; do not input a locally available manifest");
            System.Console.WriteLine(@"  output_file_name    - Name of the remuxed file in the .\output folder");
            System.Console.WriteLine(@"  -t                  - Enables execution timing; reports how long each operation took");
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
                //set required globals
                Processor.DecryptionKey = args[0];
                Processor.ManifestURL = args[1];

                //optional output file name is the third parameter
                if (args.Length > 2)
                    Processor.OutFileName =
                        args[2] != @"-t"
                            ? args[2]
                            : Processor.OutFileName;

                //begin
                var outFile = Processor.StartProcessor();

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
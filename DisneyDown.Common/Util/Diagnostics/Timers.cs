using DisneyDown.Common.Globals;
using System;
using System.Diagnostics;
using System.Linq;

// ReSharper disable InvertIf

namespace DisneyDown.Common.Util.Diagnostics
{
    public static class Timers
    {
        /// <summary>
        /// All generic timings are stored here
        /// </summary>
        public static MainTimers Generic { get; } = new MainTimers();

        /// <summary>
        /// All Disney+ intro (bumper) timings are stored here
        /// </summary>
        public static BumperTimers Bumper { get; } = new BumperTimers();

        /// <summary>
        /// Whether or not the user passed the "-t" flag to enabled diagnostics timings
        /// </summary>
        public static bool TimersEnabled
            => Environment.GetCommandLineArgs().Contains(@"-t");

        /// <summary>
        /// Formats a stopwatch's elapsed time as a string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string StopwatchStringFormat(Stopwatch s)
            => s.ElapsedMilliseconds > 0
                ? s.Elapsed.ToShortForm()
                : @"0ms";

        /// <summary>
        /// Generates the outer border sequence to wrap around the timing box
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static string MaxTimerStringLengthBorder(char c = '═')
            => new string(c, MaxTimerStringLength() + 1);

        /// <summary>
        /// Start a timer in accordance with the current environment settings
        /// </summary>
        /// <param name="s"></param>
        public static void StartTimer(Stopwatch s)
        {
            try
            {
                //only try and start the timer if timers are enabled
                if (TimersEnabled)
                    s.Start();
            }
            catch
            {
                //nothing
            }
        }

        /// <summary>
        /// Stop a timer in accordance with the current environment settings
        /// </summary>
        /// <param name="s"></param>
        public static void StopTimer(Stopwatch s)
        {
            try
            {
                //only try and stop the timer if timers are enabled
                if (TimersEnabled)
                    s.Stop();
            }
            catch
            {
                //nothing
            }
        }

        /// <summary>
        /// Maximum formatted string length of every timer in the application
        /// </summary>
        /// <returns></returns>
        private static int MaxTimerStringLength()
        {
            //store all timers in an array
            var allTimers = new[]
            {
                StopwatchStringFormat(Generic.ExecutionTimer).Length,
                StopwatchStringFormat(Generic.RemuxTimer).Length,
                StopwatchStringFormat(Generic.AudioDownloadTimer).Length,
                StopwatchStringFormat(Generic.VideoDownloadTimer).Length,
                StopwatchStringFormat(Generic.SubtitlesDownloadTimer).Length,
                StopwatchStringFormat(Generic.SubtitlesParseTimer).Length,
                StopwatchStringFormat(Generic.AudioDecryptTimer).Length,
                StopwatchStringFormat(Generic.VideoDecryptTimer).Length,
                StopwatchStringFormat(Bumper.BumperRemuxTimer).Length,
                StopwatchStringFormat(Bumper.BumperAudioDownloadTimer).Length,
                StopwatchStringFormat(Bumper.BumperVideoDownloadTimer).Length,
                StopwatchStringFormat(Bumper.BumperAudioDecryptTimer).Length,
                StopwatchStringFormat(Bumper.BumperVideoDecryptTimer).Length
            };

            //return the maximum entry
            return allTimers.Max();
        }

        /// <summary>
        /// Pads a formatted timer string with spaces
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private static string PadTimerLine(string line)
        {
            //max line length
            var maxLength = MaxTimerStringLength();

            //correct new generation length
            var amountSpaces = maxLength - line.Length;

            //return the formatted line
            return $@"{line}{new string(' ', amountSpaces)}";
        }

        /// <summary>
        /// Formats and prints all diagnostics timers to the console
        /// </summary>
        public static void ReportTimers()
        {
            try
            {
                //are diagnostics timers enabled?
                if (TimersEnabled)
                {
                    //border string
                    var borderAdd = MaxTimerStringLengthBorder();
                    var spaceAdd = MaxTimerStringLengthBorder(' ');

                    //write out the timings
                    Console.WriteLine($"╔═══════════════════════════{borderAdd}╗");
                    Console.WriteLine($"║ Execution Timings         {spaceAdd}║");
                    Console.WriteLine($"╠═════════════════════════╦═{borderAdd}╣");
                    Console.WriteLine($"║ Program Execution       ║ {PadTimerLine(StopwatchStringFormat(Generic.ExecutionTimer))} ║");
                    Console.WriteLine($"╠═════════════════════════╬═{borderAdd}╣");
                    Console.WriteLine($"║ Remux Execution         ║ {PadTimerLine(StopwatchStringFormat(Generic.RemuxTimer))} ║");
                    Console.WriteLine($"║ Audio Download          ║ {PadTimerLine(StopwatchStringFormat(Generic.AudioDownloadTimer))} ║");
                    Console.WriteLine($"║ Video Download          ║ {PadTimerLine(StopwatchStringFormat(Generic.VideoDownloadTimer))} ║");
                    Console.WriteLine($"║ Audio Decrypt           ║ {PadTimerLine(StopwatchStringFormat(Generic.AudioDecryptTimer))} ║");
                    Console.WriteLine($"║ Video Decrypt           ║ {PadTimerLine(StopwatchStringFormat(Generic.VideoDecryptTimer))} ║");
                    Console.WriteLine($"║ Subtitles Download      ║ {PadTimerLine(StopwatchStringFormat(Generic.SubtitlesDownloadTimer))} ║");
                    Console.WriteLine($"║ Subtitles Parse/Merge   ║ {PadTimerLine(StopwatchStringFormat(Generic.SubtitlesParseTimer))} ║");

                    //has the user enabled downloading the Disney+ intro (bumper)?
                    if (!Args.DownloadBumperEnabled)

                        //bumpers not enabled; close the timings box
                        Console.WriteLine($"╚═════════════════════════╩═{borderAdd}╝");
                    else
                    {
                        //bumpers enabled; report all bumper timings
                        Console.WriteLine($"╠═════════════════════════╬═{borderAdd}╣");
                        Console.WriteLine($"║ Bumper Remux Execution  ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperRemuxTimer))} ║");
                        Console.WriteLine($"║ Bumper Concat Execution ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperConcatTimer))} ║");
                        Console.WriteLine($"║ Bumper Audio Download   ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperAudioDownloadTimer))} ║");
                        Console.WriteLine($"║ Bumper Video Download   ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperVideoDownloadTimer))} ║");
                        Console.WriteLine($"║ Bumper Audio Decrypt    ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperAudioDecryptTimer))} ║");
                        Console.WriteLine($"║ Bumper Video Decrypt    ║ {PadTimerLine(StopwatchStringFormat(Bumper.BumperVideoDecryptTimer))} ║");
                        Console.WriteLine($"╚═════════════════════════╩═{borderAdd}╝");
                    }
                }
            }
            catch
            {
                //nothing
            }
        }
    }
}
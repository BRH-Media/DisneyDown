using System;
using System.Diagnostics;
using System.Linq;

// ReSharper disable InvertIf

namespace DisneyDown.Common.Util.Diagnostics
{
    public static class Timers
    {
        public static Stopwatch ExecutionTimer { get; } = new Stopwatch();
        public static bool TimersEnabled => Environment.GetCommandLineArgs().Contains(@"-t");

        private static string StopwatchMilliseconds(Stopwatch s)
            => s.ElapsedMilliseconds > 0
                ? s.Elapsed.ToShortForm()
                : @"0ms";

        private static int MaxTimerStringLength()
        {
            //store all timers in an array
            var allTimers = new[]
            {
                StopwatchMilliseconds(ExecutionTimer).Length,
                StopwatchMilliseconds(MainTimers.RemuxTimer).Length,
                StopwatchMilliseconds(MainTimers.AudioDownloadTimer).Length,
                StopwatchMilliseconds(MainTimers.VideoDownloadTimer).Length,
                StopwatchMilliseconds(MainTimers.AudioDecryptTimer).Length,
                StopwatchMilliseconds(MainTimers.VideoDecryptTimer).Length,
                StopwatchMilliseconds(BumperTimers.BumperRemuxTimer).Length,
                StopwatchMilliseconds(BumperTimers.BumperAudioDownloadTimer).Length,
                StopwatchMilliseconds(BumperTimers.BumperVideoDownloadTimer).Length,
                StopwatchMilliseconds(BumperTimers.BumperAudioDecryptTimer).Length,
                StopwatchMilliseconds(BumperTimers.BumperVideoDecryptTimer).Length
            };

            //return the maximum entry
            return allTimers.Max();
        }

        private static string FormatTimerLine(string line)
        {
            //max line length
            var maxLength = MaxTimerStringLength();

            //correct new generation length
            var amountSpaces = maxLength - line.Length;

            //return the formatted line
            return $@"{line}{new string(' ', amountSpaces)}";
        }

        private static string MaxTimerStringLengthBorder(char c = '═')
            => new string(c, MaxTimerStringLength() + 1);

        public static void ReportTimers()
        {
            try
            {
                if (TimersEnabled)
                {
                    //border string
                    var borderAdd = MaxTimerStringLengthBorder();
                    var spaceAdd = MaxTimerStringLengthBorder(' ');

                    //write out the timings
                    Console.WriteLine($"╔═══════════════════════════{borderAdd}╗");
                    Console.WriteLine($"║ Execution Timings         {spaceAdd}║");
                    Console.WriteLine($"╠═════════════════════════╦═{borderAdd}╣");
                    Console.WriteLine($"║ Program Execution       ║ {FormatTimerLine(StopwatchMilliseconds(ExecutionTimer))} ║");
                    Console.WriteLine($"╠═════════════════════════╬═{borderAdd}╣");
                    Console.WriteLine($"║ Remux Execution         ║ {FormatTimerLine(StopwatchMilliseconds(MainTimers.RemuxTimer))} ║");
                    Console.WriteLine($"║ Audio Download          ║ {FormatTimerLine(StopwatchMilliseconds(MainTimers.AudioDownloadTimer))} ║");
                    Console.WriteLine($"║ Video Download          ║ {FormatTimerLine(StopwatchMilliseconds(MainTimers.VideoDownloadTimer))} ║");
                    Console.WriteLine($"║ Audio Decrypt           ║ {FormatTimerLine(StopwatchMilliseconds(MainTimers.AudioDecryptTimer))} ║");
                    Console.WriteLine($"║ Video Decrypt           ║ {FormatTimerLine(StopwatchMilliseconds(MainTimers.VideoDecryptTimer))} ║");

                    //print bumper timings only if enabled
                    if (!Globals.DownloadBumperEnabled)
                        Console.WriteLine($"╚═════════════════════════╩═{borderAdd}╝");
                    else
                    {
                        Console.WriteLine($"╠═════════════════════════╬═{borderAdd}╣");
                        Console.WriteLine($"║ Bumper Remux Execution  ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperRemuxTimer))} ║");
                        Console.WriteLine($"║ Bumper Concat Execution ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperConcatTimer))} ║");
                        Console.WriteLine($"║ Bumper Audio Download   ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperAudioDownloadTimer))} ║");
                        Console.WriteLine($"║ Bumper Video Download   ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperVideoDownloadTimer))} ║");
                        Console.WriteLine($"║ Bumper Audio Decrypt    ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperAudioDecryptTimer))} ║");
                        Console.WriteLine($"║ Bumper Video Decrypt    ║ {FormatTimerLine(StopwatchMilliseconds(BumperTimers.BumperVideoDecryptTimer))} ║");
                        Console.WriteLine($"╚═════════════════════════╩═{borderAdd}╝");
                    }
                }
            }
            catch
            {
                //nothing
            }
        }

        public static void StartTimer(Stopwatch s)
        {
            try
            {
                if (TimersEnabled)
                    s.Start();
            }
            catch
            {
                //nothing
            }
        }

        public static void StopTimer(Stopwatch s)
        {
            try
            {
                if (TimersEnabled)
                    s.Stop();
            }
            catch
            {
                //nothing
            }
        }
    }
}
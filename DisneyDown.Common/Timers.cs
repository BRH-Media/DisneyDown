using System;
using System.Diagnostics;
using System.Linq;

namespace DisneyDown.Common
{
    public static class Timers
    {
        public static Stopwatch ExecutionTimer { get; } = new Stopwatch();
        public static Stopwatch RemuxTimer { get; } = new Stopwatch();
        public static Stopwatch VideoDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch AudioDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch VideoDecryptTimer { get; } = new Stopwatch();
        public static Stopwatch AudioDecryptTimer { get; } = new Stopwatch();

        public static bool TimersEnabled => Environment.GetCommandLineArgs().Contains(@"-t");

        public static void ReportTimers()
        {
            try
            {
                if (TimersEnabled)
                {
                    System.Console.WriteLine("Execution timings:");
                    System.Console.WriteLine($"Program execution - {(ExecutionTimer.ElapsedMilliseconds > 0 ? ExecutionTimer.Elapsed.ToShortForm() : @"0ms")}");
                    System.Console.WriteLine($"Remux execution   - {(RemuxTimer.ElapsedMilliseconds > 0 ? RemuxTimer.Elapsed.ToShortForm() : @"0ms")}");
                    System.Console.WriteLine($"Audio download    - {(AudioDownloadTimer.ElapsedMilliseconds > 0 ? AudioDownloadTimer.Elapsed.ToShortForm() : @"0ms")}");
                    System.Console.WriteLine($"Video download    - {(VideoDownloadTimer.ElapsedMilliseconds > 0 ? VideoDownloadTimer.Elapsed.ToShortForm() : @"0ms")}");
                    System.Console.WriteLine($"Audio decrypt     - {(AudioDecryptTimer.ElapsedMilliseconds > 0 ? AudioDecryptTimer.Elapsed.ToShortForm() : @"0ms")}");
                    System.Console.WriteLine($"Video decrypt     - {(VideoDecryptTimer.ElapsedMilliseconds > 0 ? VideoDecryptTimer.Elapsed.ToShortForm() : @"0ms")}");
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
using System.Diagnostics;

namespace DisneyDown.Common.Util.Diagnostics
{
    public static class MainTimers
    {
        public static Stopwatch RemuxTimer { get; } = new Stopwatch();
        public static Stopwatch VideoDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch AudioDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch VideoDecryptTimer { get; } = new Stopwatch();
        public static Stopwatch AudioDecryptTimer { get; } = new Stopwatch();
    }
}
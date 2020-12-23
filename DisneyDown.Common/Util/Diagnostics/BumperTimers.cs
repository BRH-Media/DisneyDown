using System.Diagnostics;

namespace DisneyDown.Common.Util.Diagnostics
{
    public static class BumperTimers
    {
        public static Stopwatch BumperVideoDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch BumperAudioDownloadTimer { get; } = new Stopwatch();
        public static Stopwatch BumperVideoDecryptTimer { get; } = new Stopwatch();
        public static Stopwatch BumperAudioDecryptTimer { get; } = new Stopwatch();
        public static Stopwatch BumperRemuxTimer { get; } = new Stopwatch();
    }
}
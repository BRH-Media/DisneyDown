using System.Diagnostics;

namespace DisneyDown.Common.Util.Diagnostics
{
    public class MainTimers
    {
        /// <summary>
        /// Time it takes to merge the separate streams using FFMPEG
        /// </summary>
        public Stopwatch RemuxTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to download the video stream
        /// </summary>
        public Stopwatch VideoDownloadTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to download all subtitles
        /// </summary>
        public Stopwatch SubtitlesDownloadTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to parse, merge and reformat all subtitles
        /// </summary>
        public Stopwatch SubtitlesParseTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to download the audio stream
        /// </summary>
        public Stopwatch AudioDownloadTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to decrypt the video stream
        /// </summary>
        public Stopwatch VideoDecryptTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to decrypt the audio stream
        /// </summary>
        public Stopwatch AudioDecryptTimer { get; } = new Stopwatch();

        /// <summary>
        /// Records the entire execution time (initiated in the console application not this library)
        /// </summary>
        public Stopwatch ExecutionTimer { get; } = new Stopwatch();
    }
}
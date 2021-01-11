using System.Diagnostics;

namespace DisneyDown.Common.Util.Diagnostics
{
    public class BumperTimers
    {
        /// <summary>
        /// Time it takes to download the bumper video stream
        /// </summary>
        public Stopwatch BumperVideoDownloadTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to download the bumper audio stream
        /// </summary>
        public Stopwatch BumperAudioDownloadTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to decrypt the bumper video stream
        /// </summary>
        public Stopwatch BumperVideoDecryptTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to decrypt the bumper audio stream
        /// </summary>
        public Stopwatch BumperAudioDecryptTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to merge the separate bumper streams using FFMPEG
        /// </summary>
        public Stopwatch BumperRemuxTimer { get; } = new Stopwatch();

        /// <summary>
        /// Time it takes to concatenate the main content with the bumper
        /// </summary>
        public Stopwatch BumperConcatTimer { get; } = new Stopwatch();
    }
}
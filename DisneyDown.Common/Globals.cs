using System;
using System.IO;
using System.Linq;

namespace DisneyDown.Common
{
    public static class Globals
    {
        /// <summary>
        /// Global storage for the Disney+ Manifest URL
        /// </summary>
        public static string ManifestUrl { get; set; } = @"";

        /// <summary>
        /// Global storage for the Widevine decryption key
        /// </summary>
        public static string DecryptionKey { get; set; } = @"";

        /// <summary>
        /// Global storage for the remuxed file name
        /// </summary>
        public static string OutFileName { get; set; } = @"decryptedDisney.mkv";

        /// <summary>
        /// Whether or not the application should download and decrypt the Disney+ "Original" bumper
        /// </summary>
        public static bool DownloadBumperEnabled =>
            Environment.GetCommandLineArgs().Contains(@"-b");

        /// <summary>
        /// Whether or not the application should only download video (not audio)
        /// </summary>
        public static bool VideoOnlyMode =>
            Environment.GetCommandLineArgs().Contains(@"-v");

        /// <summary>
        /// Whether or not the application should only download audio (not video)
        /// </summary>
        public static bool AudioOnlyMode =>
            Environment.GetCommandLineArgs().Contains(@"-a");

        /// <summary>
        /// Whether or not the application ignores the master parser, and tries to directly download the playlist itself
        /// </summary>
        public static bool ExclusiveMode =>
            Environment.GetCommandLineArgs().Contains(@"-e")
            && (AudioOnlyMode || VideoOnlyMode);

        /// <summary>
        /// Verifies the existence of ffmpeg and bento4 mp4decrypt.
        /// </summary>
        /// <returns></returns>
        public static bool CheckRequiredExecutables() =>
            File.Exists(@"ffmpeg.exe") && File.Exists(@"mp4decrypt.exe");
    }
}
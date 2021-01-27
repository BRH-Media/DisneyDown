using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DisneyDown.Common.Globals
{
    public static class Args
    {
        /// <summary>
        /// Stores a list of string arrays with index 0 being the argument itself and index 1 being the argument's description
        /// </summary>
        public static Dictionary<string, string> Definitions { get; } = new Dictionary<string, string>
        {
            { @"-a",    @"Forces the application to only download and decrypt audio" },
            { @"-b",    @"Enables downloading, processing and concatenating the Disney+ intro (if available)" },
            { @"-e",    @"Forces the application to avoid using the master parser; requires -v or -a but not both" },
            { @"-t",    @"Enables execution timing; reports how long each operation took" },
            { @"-v",    @"Forces the application to only download and decrypt video" },
            { @"-?",    @"Shows basic information about how to use this program" },
            { @"-help", @"Shows basic information about how to use this program" },
            { @"-h",    @"Shows basic information about how to use this program" }
        };

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
        public static bool CheckRequiredExecutables
            => File.Exists(@"ffmpeg.exe")
               && File.Exists(@"mp4decrypt.exe");
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable InconsistentNaming

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
            { @"-d",    @"Enables debug messages" },
            { @"-e",    @"Forces the application to avoid using the master parser; requires -v or -a but not both" },
            { @"-f",    @"Enables the fallback filter; use this if you're experiencing trouble with segment filtering" },
            { @"-l",    @"Disables the usage of the API module" },
            { @"-n",    @"Disables input prompts (e.g. press to continue)"},
            { @"-k",    @"Forces DisneyDown to not use the defined keyserver regardless of the config" },
            { @"-t",    @"Enables execution timing; reports how long each operation took" },
            { @"-v",    @"Forces the application to only download and decrypt video" },
            { @"-?",    @"Shows basic information about how to use this program" },
            { @"-help", @"Shows basic information about how to use this program" },
            { @"-h",    @"Shows basic information about how to use this program" }
        };

        /// <summary>
        /// Whether or not the application is allowed to use the Disney+ API
        /// </summary>
        public static bool ApiDisabled =>
            Environment.GetCommandLineArgs().Contains(@"-l");

        /// <summary>
        /// Whether or not the application should disable the usage of the defined keyserver
        /// </summary>
        public static bool KeyServerDisabled =>
            Environment.GetCommandLineArgs().Contains(@"-k");

        /// <summary>
        /// Whether or not the application should enable debug messages
        /// </summary>
        public static bool DebugModeEnabled =>
            Environment.GetCommandLineArgs().Contains(@"-d");

        /// <summary>
        /// Whether or not the application should allow the usage of the fallback filter
        /// </summary>
        public static bool FallbackFilterEnabled =>
            Environment.GetCommandLineArgs().Contains(@"-f");

        /// <summary>
        /// Whether or not the application should download and decrypt the Disney+ "Original" bumper
        /// </summary>
        public static bool DownloadBumperEnabled =>
            Environment.GetCommandLineArgs().Contains(@"-b");

        /// <summary>
        /// Whether or not the application will prompt for user input
        /// </summary>
        public static bool InputDisabled =>
            Environment.GetCommandLineArgs().Contains(@"-n");

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
        /// Check if ffmpeg.exe exists
        /// </summary>
        public static bool FFMpegExists
            => File.Exists($@"{Strings.AssemblyDirectory}\ffmpeg.exe");

        /// <summary>
        /// Check if mp4decrypt.exe exists
        /// </summary>
        public static bool MP4DecryptExists
            => File.Exists($@"{Strings.AssemblyDirectory}\mp4decrypt.exe");

        /// <summary>
        /// Check if mp4dump.exe exists
        /// </summary>
        public static bool MP4DumpExists
            => File.Exists($@"{Strings.AssemblyDirectory}\mp4dump.exe");

        /// <summary>
        /// Verifies the existence of ffmpeg and bento4 mp4decrypt.
        /// </summary>
        /// <returns></returns>
        public static bool CheckRequiredExecutables
            => FFMpegExists &&
               MP4DecryptExists &&
               MP4DumpExists;
    }
}
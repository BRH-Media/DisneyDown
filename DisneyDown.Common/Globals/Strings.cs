using System;
using System.IO;
using System.Reflection;

namespace DisneyDown.Common.Globals
{
    public static class Strings
    {
        /// <summary>
        /// Default language for audio tracks
        /// </summary>
        public static string DefaultLang { get; set; } = @"en";

        /// <summary>
        /// Default language priority file for audio tracks
        /// </summary>
        public static string AudioLangPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\audioLangPriority.cfg";

        /// <summary>
        /// Default audio group priority file
        /// </summary>
        public static string AudioGroupPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\audioGroupPriority.cfg";

        /// <summary>
        /// Default language priority file for subtitle tracks
        /// </summary>
        public static string SubtitleLangPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\subtitleLangPriority.cfg";

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
        /// Current directory of the DisneyDown executable
        /// </summary>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}
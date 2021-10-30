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
        /// Key that must be entered to trigger lookup mode
        /// </summary>
        public static string LookupModeTriggerKey { get; } = new string('0', 32);

        /// <summary>
        /// Default language priority file for audio tracks
        /// </summary>
        public static string AudioLangPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\priority\audioLangPriority.cfg";

        /// <summary>
        /// Default audio group priority file
        /// </summary>
        public static string AudioGroupPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\priority\audioGroupPriority.cfg";

        /// <summary>
        /// Default language priority file for subtitle tracks
        /// </summary>
        public static string SubtitleLangPriorityFile { get; set; } = $@"{AssemblyDirectory}\cfg\priority\subtitleLangPriority.cfg";

        /// <summary>
        /// Filter configuration file
        /// </summary>
        public static string SegmentFilterConfigurationFile { get; set; } = $@"{AssemblyDirectory}\cfg\segmentFilter.json";

        /// <summary>
        /// Bandwidth configuration file
        /// </summary>
        public static string BandwidthConfigurationFile { get; set; } = $@"{AssemblyDirectory}\cfg\bandwidthDefinitions.json";

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
namespace DisneyDown.Common.Globals
{
    public static class Strings
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
    }
}
namespace DisneyDown.WidevineCrypto.Globals
{
    public static class Args
    {
        /// <summary>
        /// Whether or not to start the application in debug mode
        /// </summary>
        public static bool DebugMode =>
            Environment.GetCommandLineArgs().Contains(@"-d");

        /// <summary>
        /// Whether or not to start the application socket connection mode
        /// </summary>
        public static bool SocketHost =>
            Environment.GetCommandLineArgs().Contains(@"-s");
    }
}
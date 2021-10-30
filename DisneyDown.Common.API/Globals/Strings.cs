using System;
using System.IO;
using System.Reflection;

namespace DisneyDown.Common.API.Globals
{
    public static class Strings
    {
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

        public static string ServicesUrl { get; set; } = @"https://bam-sdk-configs.bamgrid.com/bam-sdk/v3.0/disney-svod-3d9324fc/browser/v9.0/linux/chrome/prod.json";

        public static string ServicesFile { get; set; } = $"{AssemblyDirectory}/cfg/api/disneyPlus.services.json";
        public static string ConfigurationFile { get; set; } = $"{AssemblyDirectory}/cfg/api/disneyPlus.config.json";
    }
}
using System;
using System.IO;
using System.Reflection;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class Globals
    {
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

        public static Endpoints SystemEndpoints { get; set; } = new Endpoints();
    }
}
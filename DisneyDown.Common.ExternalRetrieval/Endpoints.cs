// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming

using System;
using System.IO;
using System.Reflection;

namespace DisneyDown.Common.ExternalRetrieval
{
    public static class Endpoints
    {
        public static string FFMpegDownloadUrl { get; set; } = @"https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip";
        public static string FFMpegChecksumUrl { get; set; } = @"https://www.gyan.dev/ffmpeg/builds/sha256-release-essentials-zip";
        public static string MP4DecryptDownloadUrl { get; set; } = @"http://zebulon.bok.net/Bento4/binaries/Bento4-SDK-1-6-0-637.x86_64-microsoft-win32.zip";
        public static string MP4DecryptChecksum { get; set; } = @"a60f19aedcdfd9bc7a9fc6a4c7587fefb4c59273";

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
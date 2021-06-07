// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

using DisneyDown.Common.Util.Kit;
using System;
using System.IO;

namespace DisneyDown.Common.ExternalRetrieval
{
    public class Endpoints
    {
        public string FFMpegDownloadUrl { get; set; } = @"https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip";
        public string FFMpegChecksumUrl { get; set; } = @"https://www.gyan.dev/ffmpeg/builds/sha256-release-essentials-zip";
        public string MP4DecryptDownloadUrl { get; set; } = @"http://zebulon.bok.net/Bento4/binaries/Bento4-SDK-1-6-0-637.x86_64-microsoft-win32.zip";
        public string MP4DecryptChecksum { get; set; } = @"a60f19aedcdfd9bc7a9fc6a4c7587fefb4c59273";
        public static string XmlFileDirectory { get; set; } = $@"{Globals.AssemblyDirectory}\cfg";

        public static void EnsureXml()
        {
            //XML file to save/load
            const string file = @"external.xml";

            //does the folder exist yet
            if (!Directory.Exists(XmlFileDirectory))

                //no, create it
                Directory.CreateDirectory(XmlFileDirectory);

            //file final path
            var filePath = $@"{XmlFileDirectory}\{file}";

            //does it not already exist?
            if (!File.Exists(filePath))

                //export current Endpoints object to XML
                new Endpoints().ToXml(filePath);
            else
            {
                //attempt to load it
                var e = FromXml(filePath);

                //validation
                if (e != null)

                    //reset global endpoints
                    Globals.SystemEndpoints = e;
            }
        }

        public static Endpoints FromXml(string xmlFile)
        {
            try
            {
                //verification
                if (!string.IsNullOrWhiteSpace(xmlFile))
                {
                    //start serialisation
                    var e = Extensions.DeserializeToObject<Endpoints>(xmlFile);

                    //report success
                    return e;
                }

                //report error
                ConsoleWriters.ConsoleWriteError(@"Couldn't load endpoints XML file: invalid parameters");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Couldn't load endpoints XML file: {ex.Message}");
            }

            //default return
            return null;
        }
    }
}
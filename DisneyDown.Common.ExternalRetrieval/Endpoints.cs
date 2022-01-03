using DisneyDown.Common.Util.Kit;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTypeModifiers
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.ExternalRetrieval
{
    public class Endpoints
    {
        public ModuleRetrievalEndpoint FFMpegEndpoint { get; set; } = new ModuleRetrievalEndpoint
        {
            DownloadUrl = @"https://www.gyan.dev/ffmpeg/builds/ffmpeg-release-essentials.zip",
            FileNames = new string[] { @"ffmpeg.exe" },
            Checksum =
            {
                Algorithm = @"sha256",
                Content = @"fetch:aHR0cHM6Ly93d3cuZ3lhbi5kZXYvZmZtcGVnL2J1aWxkcy9wYWNrYWdlcy9mZm1wZWctNC40LjEtZXNzZW50aWFsc19idWlsZC56aXAuc2hhMjU2"
            }
        };

        public ModuleRetrievalEndpoint MP4DecryptEndpoint { get; set; } = new ModuleRetrievalEndpoint
        {
            DownloadUrl = @"http://zebulon.bok.net/Bento4/binaries/Bento4-SDK-1-6-0-637.x86_64-microsoft-win32.zip",
            FileNames = new string[] { @"mp4decrypt.exe" },
            Checksum =
            {
                Algorithm = @"sha1",
                Content = @"return:YTYwZjE5YWVkY2RmZDliYzdhOWZjNmE0Yzc1ODdmZWZiNGM1OTI3Mw=="
            }
        };

        public ModuleRetrievalEndpoint MP4DumpEndpoint { get; set; } = new ModuleRetrievalEndpoint
        {
            DownloadUrl = @"http://zebulon.bok.net/Bento4/binaries/Bento4-SDK-1-6-0-637.x86_64-microsoft-win32.zip",
            FileNames = new string[] { @"mp4dump.exe" },
            Checksum =
            {
                Algorithm = @"sha1",
                Content = @"return:YTYwZjE5YWVkY2RmZDliYzdhOWZjNmE0Yzc1ODdmZWZiNGM1OTI3Mw=="
            }
        };

        public static string JsonFileDirectory { get; set; } = $@"{Globals.AssemblyDirectory}\cfg";

        public static void EnsureJson()
        {
            //JSON file to save/load
            const string file = @"externalModules.json";

            //does the folder exist yet
            if (!Directory.Exists(JsonFileDirectory))

                //no, create it
                Directory.CreateDirectory(JsonFileDirectory);

            //file final path
            var filePath = $@"{JsonFileDirectory}\{file}";

            //does it not already exist?
            if (!File.Exists(filePath))

                //export current Endpoints object to JSON
                new Endpoints().ToJson(filePath);
            else
            {
                //attempt to load it
                var e = FromJson(filePath);

                //validation
                if (e != null)

                    //reset global endpoints
                    Globals.SystemEndpoints = e;
            }
        }

        public static Endpoints FromJson(string jsonFile)
        {
            try
            {
                //verification
                if (!string.IsNullOrWhiteSpace(jsonFile))
                {
                    //validation
                    if (File.Exists(jsonFile))
                    {
                        //start serialisation
                        var e = Extensions.DeserializeToObject<Endpoints>(File.ReadAllText(jsonFile));

                        //report success
                        return e;
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Tried to read endpoints JSON file but it doesn't exist");
                    }
                }

                //report error
                ConsoleWriters.ConsoleWriteError(@"Couldn't load endpoints JSON file: invalid parameters");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Couldn't load endpoints JSON file: {ex.Message}");
            }

            //default return
            return null;
        }
    }
}
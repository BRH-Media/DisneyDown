using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.IO;

namespace DisneyDown.Common.Processors.Downloaders.Audio
{
    public static class BumperAudioDownloader
    {
        public static string DownloadBumperAudioFromPlaylist(string playlist, string playlistUri, string encryptedBumperAudioFile = "bumperAudioEncrypted.bin")
        {
            try
            {
                //start measuring bumper audio download time
                Timers.StartTimer(BumperTimers.BumperAudioDownloadTimer);

                //base URI for the audio playlist
                var audioBaseUri = Methods.GetBaseUrl(playlistUri);

                //bumper map
                var audioMapPath = ManifestParsers.ManifestBumperMapUrl(playlist);

                //construct full map URL
                var audioMapUrl = $"{audioBaseUri}{audioMapPath}";

                //report status
                Console.WriteLine($@"Downloading bumper audio MPEG-4 init segment: {audioMapUrl}");

                //download the bumper audio map
                var audioMap = ResourceGrab.GrabBytes(audioMapUrl);

                //validation
                if (audioMap != null)
                {
                    //write to file
                    File.WriteAllBytes(encryptedBumperAudioFile, audioMap);

                    //start segments download
                    Console.WriteLine(@"Audio bumper init data saved successfully; starting segments download");

                    //do download
                    SegmentHandlers.DownloadAllSegments(playlist, audioBaseUri,
                        encryptedBumperAudioFile, @"-BUMPER/");

                    //report success
                    Console.WriteLine(
                        $"\nSuccessfully downloaded bumper audio data to: {encryptedBumperAudioFile}\n");

                    //stop measuring bumper audio download time
                    Timers.StopTimer(BumperTimers.BumperAudioDownloadTimer);

                    //return the saved file path
                    return encryptedBumperAudioFile;
                }

                Console.WriteLine(@"Bumper audio download failed; audio init segment data was null");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bumper audio download error: {ex}");
            }

            //stop measuring bumper audio download time
            Timers.StopTimer(BumperTimers.BumperAudioDownloadTimer);

            //default
            return @"";
        }
    }
}
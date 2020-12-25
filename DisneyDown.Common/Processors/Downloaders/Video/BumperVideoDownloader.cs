using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.IO;

namespace DisneyDown.Common.Processors.Downloaders.Video
{
    public static class BumperVideoDownloader
    {
        public static string DownloadBumperVideoFromPlaylist(string playlist, string playlistUri, string encryptedBumperVideoFile = "bumperVideoEncrypted.bin")
        {
            try
            {
                //start measuring bumper video download time
                Timers.StartTimer(BumperTimers.BumperVideoDownloadTimer);

                //base URI for the video playlist
                var videoBaseUri = Methods.GetBaseUrl(playlistUri);

                //bumper map
                var videoMapPath = ManifestParsers.ManifestBumperMapUrl(playlist);

                //construct full map URL
                var videoMapUrl = $"{videoBaseUri}{videoMapPath}";

                //report status
                Console.WriteLine($@"Downloading bumper video MPEG-4 init segment: {videoMapUrl}");

                //download the bumper video map
                var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                //validation
                if (videoMap != null)
                {
                    //write to file
                    File.WriteAllBytes(encryptedBumperVideoFile, videoMap);

                    //start segments download
                    Console.WriteLine(@"Video bumper init data saved successfully; starting segments download");

                    //do download
                    SegmentHandlers.DownloadAllSegments(playlist, videoBaseUri,
                        encryptedBumperVideoFile, @"-BUMPER/");

                    //report success
                    Console.WriteLine(
                        $"\nSuccessfully downloaded bumper video data to: {encryptedBumperVideoFile}\n");

                    //stop measuring bumper video download time
                    Timers.StopTimer(BumperTimers.BumperVideoDownloadTimer);

                    //return the saved file path
                    return encryptedBumperVideoFile;
                }

                Console.WriteLine(@"Bumper video download failed; audio init segment data was null");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bumper audio download error: {ex}");
            }

            //stop measuring bumper video download time
            Timers.StopTimer(BumperTimers.BumperVideoDownloadTimer);

            //default
            return @"";
        }
    }
}
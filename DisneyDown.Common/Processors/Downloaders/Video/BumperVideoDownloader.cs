using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Downloaders.Video
{
    public static class BumperVideoDownloader
    {
        public static string DownloadBumperVideoFromPlaylist(string playlist, string playlistUri, string encryptedBumperVideoFile = "bumperVideoEncrypted.bin")
        {
            try
            {
                //start measuring bumper video download time
                Timers.StartTimer(Timers.Bumper.BumperVideoDownloadTimer);

                //ensure there is a bumper to download
                if (playlist.Contains(Verification.BumperIntro))
                {
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
                        SegmentHandlers.DownloadAllMpegSegments(playlist, videoBaseUri,
                            Verification.BumperIntro, encryptedBumperVideoFile);

                        //report success
                        Console.WriteLine(
                            $"\nSuccessfully downloaded bumper video data to: {encryptedBumperVideoFile}\n");

                        //stop measuring bumper video download time
                        Timers.StopTimer(Timers.Bumper.BumperVideoDownloadTimer);

                        //return the saved file path
                        return encryptedBumperVideoFile;
                    }
                    else

                        //map file was null
                        Console.WriteLine(@"Bumper video download failed; video init segment data was null");
                }
                else

                    //no bumper media detected
                    Console.WriteLine(@"Bumper video download failed; content does not contain the Disney+ bumper intro");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Bumper video download error: {ex}");
            }

            //stop measuring bumper video download time
            Timers.StopTimer(Timers.Bumper.BumperVideoDownloadTimer);

            //default
            return @"";
        }
    }
}
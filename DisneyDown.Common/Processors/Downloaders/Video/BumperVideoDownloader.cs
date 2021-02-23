using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
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
                    ConsoleWriters.WriteLine($@"[i] Downloading bumper video MPEG-4 init segment: {videoMapUrl}", ConsoleColor.Cyan);

                    //download the bumper video map
                    var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                    //validation
                    if (videoMap != null)
                    {
                        //write to file
                        File.WriteAllBytes(encryptedBumperVideoFile, videoMap);

                        //start segments download
                        ConsoleWriters.WriteLine(@"[i] Video bumper init data saved successfully; starting segments download", ConsoleColor.Green);

                        //do download
                        SegmentHandlers.DownloadAllMpegSegments(playlist, videoBaseUri,
                            Verification.BumperIntro, encryptedBumperVideoFile,
                            @"[Bumper Video]");

                        //report success
                        ConsoleWriters.WriteLine(
                            $"\n[i] Successfully downloaded bumper video data to: {encryptedBumperVideoFile}\n", ConsoleColor.Green);

                        //stop measuring bumper video download time
                        Timers.StopTimer(Timers.Bumper.BumperVideoDownloadTimer);

                        //return the saved file path
                        return encryptedBumperVideoFile;
                    }
                    else

                        //map file was null
                        ConsoleWriters.WriteLine(@"[!] Bumper video download failed; video init segment data was null", ConsoleColor.Red);
                }
                else

                    //no bumper media detected
                    ConsoleWriters.WriteLine(@"[!] Bumper video download failed; content does not contain the Disney+ bumper intro", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($"[!] Bumper video download error: {ex}", ConsoleColor.Red);
            }

            //stop measuring bumper video download time
            Timers.StopTimer(Timers.Bumper.BumperVideoDownloadTimer);

            //default
            return @"";
        }
    }
}
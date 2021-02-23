using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Downloaders.Audio
{
    public static class BumperAudioDownloader
    {
        public static string DownloadBumperAudioFromPlaylist(string playlist, string playlistUri, string encryptedBumperAudioFile = "bumperAudioEncrypted.bin")
        {
            try
            {
                //start measuring bumper audio download time
                Timers.StartTimer(Timers.Bumper.BumperAudioDownloadTimer);

                //ensure there is a bumper to download
                if (playlist.Contains(Verification.BumperIntro))
                {
                    //base URI for the audio playlist
                    var audioBaseUri = Methods.GetBaseUrl(playlistUri);

                    //bumper map
                    var audioMapPath = ManifestParsers.ManifestBumperMapUrl(playlist);

                    //construct full map URL
                    var audioMapUrl = $"{audioBaseUri}{audioMapPath}";

                    //report status
                    ConsoleWriters.WriteLine($@"[i] Downloading bumper audio MPEG-4 init segment: {audioMapUrl}", ConsoleColor.Cyan);

                    //download the bumper audio map
                    var audioMap = ResourceGrab.GrabBytes(audioMapUrl);

                    //validation
                    if (audioMap != null)
                    {
                        //write to file
                        File.WriteAllBytes(encryptedBumperAudioFile, audioMap);

                        //start segments download
                        ConsoleWriters.WriteLine(@"[i] Audio bumper init data saved successfully; starting segments download", ConsoleColor.Cyan);

                        //do download
                        SegmentHandlers.DownloadAllMpegSegments(playlist, audioBaseUri,
                            Verification.BumperIntro, encryptedBumperAudioFile,
                            @"[Bumper Audio]");

                        //report success
                        ConsoleWriters.WriteLine(
                            $"\n[i] Successfully downloaded bumper audio data to: {encryptedBumperAudioFile}\n", ConsoleColor.Green);

                        //stop measuring bumper audio download time
                        Timers.StopTimer(Timers.Bumper.BumperAudioDownloadTimer);

                        //return the saved file path
                        return encryptedBumperAudioFile;
                    }
                    else

                        //map file was null
                        ConsoleWriters.WriteLine(@"Bumper audio download failed; audio init segment data was null", ConsoleColor.Red);
                }
                else

                    //no bumper media detected
                    ConsoleWriters.WriteLine(@"Bumper audio download failed; content does not contain the Disney+ bumper intro", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($"Bumper audio download error: {ex}", ConsoleColor.Red);
            }

            //stop measuring bumper audio download time
            Timers.StopTimer(Timers.Bumper.BumperAudioDownloadTimer);

            //default
            return @"";
        }
    }
}
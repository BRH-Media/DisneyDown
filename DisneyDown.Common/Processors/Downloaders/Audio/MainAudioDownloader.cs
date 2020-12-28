using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Downloaders.Audio
{
    /// <summary>
    /// Contains methods used for downloading and saving audio data
    /// </summary>
    public static class MainAudioDownloader
    {
        /// <summary>
        /// Downloads the entire best audio quality playlist and returns the path of the saved file
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="masterPlaylistUrl"></param>
        /// <param name="encryptedAudioFile"></param>
        /// <returns></returns>
        public static string DownloadBestAudioFromMaster(string masterPlaylist, string masterPlaylistUrl, string encryptedAudioFile = "audioEncrypted.bin")
        {
            try
            {
                //validation
                if (ManifestParsers.ManifestValid(masterPlaylist))
                {
                    //if the file already exists, simply return it and don't try and re-download it
                    if (File.Exists(encryptedAudioFile))
                    {
                        Console.WriteLine($"Using existing {encryptedAudioFile}; download skipped");
                        return encryptedAudioFile;
                    }

                    if (!Args.ExclusiveMode)
                    {
                        //find best audio playlist
                        var bestAudioPlaylist = MasterParsers.MasterAudioPlaylistUri(masterPlaylist);

                        //validation
                        if (!string.IsNullOrWhiteSpace(bestAudioPlaylist))
                        {
                            //report progress
                            Console.WriteLine($"Found best audio quality manifest: {bestAudioPlaylist}");

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var audioPlaylistUrl = $"{masterBaseUri}{bestAudioPlaylist}";

                            //do the download
                            Console.WriteLine(@"Downloading audio manifest");
                            var audioManifest = ManifestParsers.DownloadManifest(audioPlaylistUrl);

                            //download processor
                            return PerformDownload(audioManifest, audioPlaylistUrl, encryptedAudioFile);
                        }
                        else
                            Console.WriteLine(@"Audio download failed; audio playlist URL was null");
                    }
                    else

                        //do the download
                        return PerformDownload(masterPlaylist, masterPlaylistUrl, encryptedAudioFile);
                }
                else
                    Console.WriteLine(@"Audio download failed; master playlist does not conform");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Audio download error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static string PerformDownload(string audioManifest, string audioPlaylistUrl, string encryptedAudioFile = "audioEncrypted.bin")
        {
            try
            {
                //start measuring audio download time
                Timers.StartTimer(MainTimers.AudioDownloadTimer);

                //validation
                if (ManifestParsers.ManifestValid(audioManifest))
                {
                    //get map URL
                    var audioBaseUri = Methods.GetBaseUrl(audioPlaylistUrl);
                    var audioMapPath = ManifestParsers.ManifestMainMapUrl(audioManifest);

                    //bumper file name
                    var bumperEncryptedFileName = $@"{Path.GetDirectoryName(encryptedAudioFile)}\bumperAudioEncrypted.bin";

                    //download the bumper if we're allowed to
                    if (Args.DownloadBumperEnabled)
                        BumperAudioDownloader.DownloadBumperAudioFromPlaylist(audioManifest,
                            audioPlaylistUrl, bumperEncryptedFileName);

                    //ensure the map URL is valid
                    if (!string.IsNullOrWhiteSpace(audioMapPath))
                    {
                        //construct full map URL
                        var audioMapUrl = $"{audioBaseUri}{audioMapPath}";

                        //download map
                        Console.WriteLine($@"Downloading audio MPEG-4 init segment: {audioMapUrl}");
                        var audioMap = ResourceGrab.GrabBytes(audioMapUrl);

                        //validation
                        if (audioMap != null)
                        {
                            //write to file
                            File.WriteAllBytes(encryptedAudioFile, audioMap);

                            //start segments download
                            Console.WriteLine(@"Audio init data saved successfully; starting segments download");

                            //do download
                            SegmentHandlers.DownloadAllSegments(audioManifest, audioBaseUri,
                                Verification.MainContent, encryptedAudioFile);

                            //report success
                            Console.WriteLine(
                                $"\nSuccessfully downloaded audio data to: {encryptedAudioFile}\n");

                            //stop measuring audio download time
                            Timers.StopTimer(MainTimers.AudioDownloadTimer);

                            //return the saved file path
                            return encryptedAudioFile;
                        }
                        else
                            Console.WriteLine(@"Audio download failed; audio init segment data was null");
                    }
                    else
                        Console.WriteLine(@"Audio download failed; audio init URL was invalid, null or empty result.");
                }
                else
                    Console.WriteLine(@"Audio download failed; audio manifest does not conform");
            }
            catch
            {
                //nothing
            }

            //stop measuring audio download time
            Timers.StopTimer(MainTimers.AudioDownloadTimer);

            //default
            return @"";
        }
    }
}
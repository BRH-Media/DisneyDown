using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
using System;
using System.IO;

// ReSharper disable LocalizableElement
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
                        ConsoleWriters.WriteLine($@"[i] Using existing {encryptedAudioFile}; download skipped", ConsoleColor.Cyan);
                        return encryptedAudioFile;
                    }

                    if (!Args.ExclusiveMode)
                    {
                        //find best audio playlist
                        var (bestAudioPlaylistPath, qualityRating) = MasterParsers.MasterAudioPlaylistUri(masterPlaylist);

                        //validation
                        if (!string.IsNullOrWhiteSpace(bestAudioPlaylistPath))
                        {
                            //report progress
                            ConsoleWriters.WriteLine($@"[i] Found best audio quality manifest: {qualityRating.QualityName}", ConsoleColor.Green);

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var audioPlaylistUrl = $"{masterBaseUri}{bestAudioPlaylistPath}";

                            //do the download
                            ConsoleWriters.WriteLine(@"[i] Downloading audio manifest", ConsoleColor.Cyan);
                            var audioManifest = ManifestParsers.DownloadManifest(audioPlaylistUrl);

                            //download processor
                            return PerformDownload(audioManifest, audioPlaylistUrl, encryptedAudioFile);
                        }
                        else

                            //report error
                            ConsoleWriters.WriteLine(@"[!] Audio download failed; audio playlist URL was null", ConsoleColor.Red);
                    }
                    else

                        //do the download
                        return PerformDownload(masterPlaylist, masterPlaylistUrl, encryptedAudioFile);
                }
                else
                    //report error
                    ConsoleWriters.WriteLine(@"[!] Audio download failed; master playlist does not conform", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Audio download error: {ex.Message}", ConsoleColor.Red);
            }

            //default
            return @"";
        }

        public static string PerformDownload(string audioManifest, string audioPlaylistUrl, string encryptedAudioFile = "audioEncrypted.bin")
        {
            try
            {
                //start measuring audio download time
                Timers.StartTimer(Timers.Generic.AudioDownloadTimer);

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
                        ConsoleWriters.WriteLine($@"[i] Downloading audio MPEG-4 init segment: {audioMapUrl}", ConsoleColor.Cyan);
                        var audioMap = ResourceGrab.GrabBytes(audioMapUrl);

                        //validation
                        if (audioMap != null)
                        {
                            //write to file
                            File.WriteAllBytes(encryptedAudioFile, audioMap);

                            //start segments download
                            ConsoleWriters.WriteLine(@"[i] Audio init data saved successfully; starting segments download", ConsoleColor.Green);

                            //do download
                            SegmentHandlers.DownloadAllMpegSegments(audioManifest, audioBaseUri,
                                Verification.MainContent, encryptedAudioFile,
                                @"[Main Audio]");

                            //report success
                            Console.WriteLine(
                                $"\n[i] Successfully downloaded audio data to: {encryptedAudioFile}\n", ConsoleColor.Green);

                            //stop measuring audio download time
                            Timers.StopTimer(Timers.Generic.AudioDownloadTimer);

                            //return the saved file path
                            return encryptedAudioFile;
                        }
                        else

                            //report error
                            ConsoleWriters.WriteLine(@"[!] Audio download failed; audio init segment data was null", ConsoleColor.Red);
                    }
                    else

                        //report error
                        ConsoleWriters.WriteLine(@"[!] Audio download failed; audio init URL was invalid, null or empty result.", ConsoleColor.Red);
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Audio download failed; audio manifest does not conform", ConsoleColor.Red);
            }
            catch
            {
                //nothing
            }

            //stop measuring audio download time
            Timers.StopTimer(Timers.Generic.AudioDownloadTimer);

            //default
            return @"";
        }
    }
}
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

namespace DisneyDown.Common.Processors.Downloaders.Video
{
    /// <summary>
    /// Contains methods used for downloading and saving video data
    /// </summary>
    public static class MainVideoDownloader
    {
        /// <summary>
        /// Downloads the entire best video quality playlist and returns the path of the saved file
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="masterPlaylistUrl"></param>
        /// <param name="encryptedVideoFile"></param>
        /// <returns></returns>
        public static string DownloadBestVideoFromMaster(string masterPlaylist, string masterPlaylistUrl,
            string encryptedVideoFile = "videoEncrypted.bin")
        {
            try
            {
                //validation
                if (ManifestParsers.ManifestValid(masterPlaylist))
                {
                    //if the file already exists, simply return it and don't try and re-download it
                    if (File.Exists(encryptedVideoFile))
                    {
                        ConsoleWriters.ConsoleWriteInfo($@"Using existing {encryptedVideoFile}; download skipped");
                        return encryptedVideoFile;
                    }

                    if (!Args.ExclusiveMode)
                    {
                        //find best video playlist
                        var (bestVideoPlaylistPath, qualityRating) = MasterParsers.MasterVideoPlaylistUri(masterPlaylist);

                        //validation
                        if (!string.IsNullOrWhiteSpace(bestVideoPlaylistPath))
                        {
                            //report progress
                            ConsoleWriters.ConsoleWriteSuccess($@"Found best video quality manifest: {qualityRating.QualityName}");

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var videoPlaylistUrl = $"{masterBaseUri}{bestVideoPlaylistPath}";

                            //report progress
                            ConsoleWriters.ConsoleWriteInfo(@"Downloading video manifest");

                            //do the download
                            var videoManifest = ManifestParsers.DownloadManifest(videoPlaylistUrl);

                            //download processor
                            return PerformDownload(videoManifest, videoPlaylistUrl, encryptedVideoFile);
                        }
                        else

                            //report error
                            ConsoleWriters.ConsoleWriteError(@"Video download failed; video playlist URL was null");
                    }
                    else

                        //do the download
                        return PerformDownload(masterPlaylist, masterPlaylistUrl, encryptedVideoFile);
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Video download failed; master playlist does not conform");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Video download error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static string PerformDownload(string videoManifest, string videoPlaylistUrl, string encryptedVideoFile = "videoEncrypted.bin")
        {
            try
            {
                //start measuring video download time
                Timers.StartTimer(Timers.Generic.VideoDownloadTimer);

                //validation
                if (ManifestParsers.ManifestValid(videoManifest))
                {
                    //get map URL
                    var videoBaseUri = Methods.GetBaseUrl(videoPlaylistUrl);
                    var videoMapPath = ManifestParsers.ManifestMainMapUrl(videoManifest);

                    //bumper file name
                    var bumperEncryptedFileName = $@"{Path.GetDirectoryName(encryptedVideoFile)}\bumperVideoEncrypted.bin";

                    //download the bumper if we're allowed to
                    if (Args.DownloadBumperEnabled)
                        BumperVideoDownloader.DownloadBumperVideoFromPlaylist(videoManifest,
                            videoPlaylistUrl, bumperEncryptedFileName);

                    //ensure the map URL is valid
                    if (!string.IsNullOrWhiteSpace(videoMapPath))
                    {
                        //construct full map URL
                        var videoMapUrl = $"{videoBaseUri}{videoMapPath}";

                        //download map
                        ConsoleWriters.ConsoleWriteInfo($@"Downloading video MPEG-4 init segment: {videoMapUrl}");
                        var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                        //validation
                        if (videoMap != null)
                        {
                            //write to file
                            File.WriteAllBytes(encryptedVideoFile, videoMap);

                            //start segments download
                            ConsoleWriters.ConsoleWriteSuccess(@"Video init data saved successfully; starting segments download");

                            //path to save key ID to
                            var kidPath = $@"{Path.GetDirectoryName(encryptedVideoFile)}\keyinfo\keyId";

                            //attempt key ID dump
                            var kid = External.GetKeyIdFromMp4(encryptedVideoFile);

                            //ensure valid KID
                            if (!string.IsNullOrWhiteSpace(kid) && kid.Length == 32)
                            {
                                //save key ID
                                External.WriteKeyIdFile(kid, kidPath);

                                //report key ID
                                ConsoleWriters.ConsoleWriteInfo($"Widevine Key ID: {kid}");

                                //pause stopwatch
                                Timers.StopTimer(Timers.Generic.VideoDownloadTimer);

                                //confirmation
                                ConsoleWriters.ConsoleWriteQuestion(@"Confirm key ID is correct");

                                //temporarily halt
                                ConsoleTools.PauseExecution();

                                //key that must be entered to trigger lookup mode
                                var lookupModeTrigger = new string('0', 32);

                                //report key or lookup key (if enabled)
                                if (Objects.KeyServerConnection.Service.ServiceEnabled)
                                {
                                    //report
                                    if (Strings.DecryptionKey != lookupModeTrigger)
                                    {
                                        //status update
                                        ConsoleWriters.ConsoleWriteInfo(@"Reporting to key server");

                                        //perform report
                                        Objects.KeyServerConnection.ReportKey(kid, Strings.DecryptionKey);
                                    }
                                    else
                                    {
                                        //status update
                                        ConsoleWriters.ConsoleWriteInfo(@"Discovering keys from server");

                                        //key result
                                        var key = Objects.KeyServerConnection.FindKey(kid);

                                        //validation
                                        if (!string.IsNullOrWhiteSpace(key?.WvKey))
                                        {
                                            //override key
                                            Strings.DecryptionKey = key.WvKey;

                                            //report status
                                            ConsoleWriters.ConsoleWriteSuccess($@"Discovered key: {key.WvKey}");
                                        }
                                    }
                                }

                                //start stopwatch
                                Timers.StartTimer(Timers.Generic.VideoDownloadTimer);

                                //do download
                                SegmentHandlers.DownloadAllMpegSegments(videoManifest, videoBaseUri,
                                    Verification.MainContent, encryptedVideoFile,
                                    @"[Main Video]");

                                //report success
                                ConsoleWriters.ConsoleWriteSuccess(
                                    $"Successfully downloaded video data to: {encryptedVideoFile}\n");

                                //stop measuring video download time
                                Timers.StopTimer(Timers.Generic.VideoDownloadTimer);

                                //return the saved file path
                                return encryptedVideoFile;
                            }
                            else

                                //report error
                                ConsoleWriters.ConsoleWriteError(@"Failed to retrieve key ID");
                        }
                        else

                            //report error
                            ConsoleWriters.ConsoleWriteError(@"Video download failed; video init segment data was null");
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Video download failed; video init URL was invalid, null or empty result.");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Video download failed; video manifest does not conform");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Video download error: {ex.Message}");
            }

            //stop measuring video download time
            Timers.StopTimer(Timers.Generic.VideoDownloadTimer);

            //default
            return @"";
        }
    }
}
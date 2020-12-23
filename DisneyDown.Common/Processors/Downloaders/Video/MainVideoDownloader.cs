using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Downloaders.Audio;
using DisneyDown.Common.Processors.Parsers;
using DisneyDown.Common.Util;
using System;
using System.IO;

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
                        Console.WriteLine($"Using existing {encryptedVideoFile}; download skipped");
                        return encryptedVideoFile;
                    }

                    if (!Globals.ExclusiveMode)
                    {
                        //find best video playlist
                        var bestVideoPlaylistUri = MasterParsers.MasterVideoPlaylistUri(masterPlaylist);

                        //validation
                        if (!string.IsNullOrWhiteSpace(bestVideoPlaylistUri))
                        {
                            //report progress
                            Console.WriteLine($"Found best video quality manifest: {bestVideoPlaylistUri}");

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var videoPlaylistUrl = $"{masterBaseUri}{bestVideoPlaylistUri}";

                            //report progress
                            Console.WriteLine(@"Downloading video manifest");

                            //do the download
                            var videoManifest = ManifestParsers.DownloadManifest(videoPlaylistUrl);

                            //download processor
                            PerformDownload(videoManifest, videoPlaylistUrl, encryptedVideoFile);
                        }
                        else
                            Console.WriteLine(@"Video download failed; video playlist URL was null");
                    }
                    else
                        //do the download
                        PerformDownload(masterPlaylist, masterPlaylistUrl, encryptedVideoFile);
                }
                else
                    Console.WriteLine(@"Video download failed; master playlist does not conform");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Video download error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static string PerformDownload(string videoManifest, string videoPlaylistUrl, string encryptedVideoFile = "videoEncrypted.bin")
        {
            try
            {
                //validation
                if (ManifestParsers.ManifestValid(videoManifest))
                {
                    //get map URL
                    var videoBaseUri = Methods.GetBaseUrl(videoPlaylistUrl);
                    var videoMapPath = ManifestParsers.ManifestMainMapUrl(videoManifest);

                    //bumper file name
                    var bumperEncryptedFileName = $@"{Path.GetDirectoryName(encryptedVideoFile)}\bumperVideoEncrypted.bin";

                    //download the bumper if we're allowed to
                    if (Globals.DownloadBumperEnabled)
                        BumperAudioDownloader.DownloadBumperAudioFromPlaylist(videoManifest,
                            videoPlaylistUrl, bumperEncryptedFileName);

                    //ensure the map URL is valid
                    if (!string.IsNullOrWhiteSpace(videoMapPath))
                    {
                        //construct full map URL
                        var videoMapUrl = $"{videoBaseUri}{videoMapPath}";

                        //download map
                        Console.WriteLine($@"Downloading video MPEG-4 init segment: {videoMapUrl}");
                        var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                        //validation
                        if (videoMap != null)
                        {
                            //write to file
                            File.WriteAllBytes(encryptedVideoFile, videoMap);

                            //start segments download
                            Console.WriteLine(@"Video init data saved successfully; starting segments download");

                            //do download
                            SegmentHandlers.DownloadAllSegments(videoManifest, videoBaseUri,
                                encryptedVideoFile);

                            //report success
                            Console.WriteLine(
                                $"\nSuccessfully downloaded video data to: {encryptedVideoFile}\n");

                            //return the saved file path
                            return encryptedVideoFile;
                        }
                        else
                        {
                            Console.WriteLine(@"Video download failed; video init segment data was null");
                        }
                    }
                    else
                        Console.WriteLine(@"Video download failed; video init URL was invalid, null or empty result.");
                }
                else
                    Console.WriteLine(@"Video download failed; video manifest does not conform");
            }
            catch
            {
                //nothing
            }

            //default
            return @"";
        }
    }
}
using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers;
using DisneyDown.Common.Util;
using System;
using System.IO;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Downloaders
{
    /// <summary>
    /// Contains methods used for downloading and saving video data
    /// </summary>
    public static class VideoDownloader
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

                    //find best video playlist
                    var bestVideoPlaylist = MasterParsers.MasterVideoPlaylist(masterPlaylist);

                    //validation
                    if (!string.IsNullOrWhiteSpace(bestVideoPlaylist))
                    {
                        //report progress
                        Console.WriteLine($"Found best video quality manifest: {bestVideoPlaylist}");

                        //create fully-qualified URL for the playlist
                        var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                        var videoPlaylistUrl = $"{masterBaseUri}{bestVideoPlaylist}";

                        //do the download
                        Console.WriteLine(@"Downloading video manifest");
                        var videoManifest = ManifestParsers.DownloadManifest(videoPlaylistUrl);

                        //validation
                        if (ManifestParsers.ManifestValid(videoManifest))
                        {
                            //get map URL
                            var videoBaseUri = Methods.GetBaseUrl(videoPlaylistUrl);
                            var videoMapPath = ManifestParsers.ManifestMapUrl(videoManifest);

                            //ensure the map URL is valid
                            if (!string.IsNullOrWhiteSpace(videoMapPath))
                            {
                                //construct full map URL
                                var videoMapUrl = $"{videoBaseUri}{videoMapPath}";

                                //download map
                                Console.WriteLine($@"Downloading MPEG-4 init segment: {videoMapUrl}");
                                var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                                //validation
                                if (videoMap != null)
                                {
                                    //write to file
                                    File.WriteAllBytes(encryptedVideoFile, videoMap);

                                    //start segments download
                                    Console.WriteLine(@"Init data saved successfully; starting segments download");

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
                    else
                        Console.WriteLine(@"Video download failed; video playlist URL was null");
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
    }
}
using DisneyDown.Common.Globals;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using System;
using System.IO;

// ReSharper disable UnusedMember.Global
// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Downloaders.Subtitles
{
    public static class MainSubtitlesDownloader
    {
        /// <summary>
        /// Downloads all subtitles and returns the directory of the saved segments (subtitles do not get merged here)
        /// </summary>
        /// <param name="masterPlaylist"></param>
        /// <param name="masterPlaylistUrl"></param>
        /// <param name="subtitlesMergeFile"></param>
        /// <returns></returns>
        public static string DownloadSubtitlesFromMaster(string masterPlaylist, string masterPlaylistUrl, string subtitlesMergeFile = "subtitles.srt")
        {
            try
            {
                //validation
                if (ManifestParsers.ManifestValid(masterPlaylist))
                {
                    //if the file already exists, simply return it and don't try and re-download it
                    if (File.Exists(subtitlesMergeFile))
                    {
                        Console.WriteLine($@"Using existing {subtitlesMergeFile}; download skipped");
                        return subtitlesMergeFile;
                    }

                    //exclusive mode determines whether to treat the 'masterPlaylist' as a normal playlist instead
                    if (!Args.ExclusiveMode)
                    {
                        //find subtitle playlist
                        var subtitlePlaylistPathUrl = MasterParsers.MasterSubtitleUrl(masterPlaylist);

                        //validation
                        if (!string.IsNullOrWhiteSpace(subtitlePlaylistPathUrl))
                        {
                            //report progress
                            Console.WriteLine($@"Found subtitles manifest: {subtitlePlaylistPathUrl}");

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var subtitlePlaylistFullUrl = $"{masterBaseUri}{subtitlePlaylistPathUrl}";

                            //do the download
                            Console.WriteLine(@"Downloading subtitles manifest");
                            var audioManifest = ManifestParsers.DownloadManifest(subtitlePlaylistFullUrl);

                            //download processor
                            return PerformDownload(audioManifest, subtitlePlaylistFullUrl, subtitlesMergeFile);
                        }
                        else

                            //report error
                            Console.WriteLine(@"Subtitles download failed; subtitles playlist URL was null");
                    }
                    else

                        //report error; subtitles cannot be downloaded in exclusive mode
                        Console.WriteLine("\nSubtitles download failed; subtitles cannot be downloaded in exclusive mode\n");
                }
                else

                    //report error
                    Console.WriteLine(@"Subtitles download failed; master playlist does not conform");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($@"Subtitles download error: {ex.Message}");
            }

            //default
            return @"";
        }

        public static string PerformDownload(string subtitlesManifest, string subtitlesManifestUrl, string subtitlesMergeFile = "subtitles.srt")
        {
            try
            {
                //start measuring subtitles download time
                Timers.StartTimer(Timers.Generic.SubtitlesDownloadTimer);

                //validation
                if (ManifestParsers.ManifestValid(subtitlesManifest))
                {
                    //get base path
                    var audioBaseUri = Methods.GetBaseUrl(subtitlesManifestUrl);

                    //do download
                    var savedDirectory = SegmentHandlers.DownloadAllSubtitlesSegments(subtitlesManifest, audioBaseUri,
                        Verification.MainContent, subtitlesMergeFile);

                    //report success
                    Console.WriteLine(
                        $"\nSuccessfully downloaded subtitles data to: {savedDirectory}\n");

                    //stop measuring audio download time
                    Timers.StopTimer(Timers.Generic.SubtitlesDownloadTimer);

                    //return the saved files directory
                    return savedDirectory;
                }
                else
                    Console.WriteLine(@"Subtitles download failed; subtitles manifest does not conform");
            }
            catch
            {
                //nothing
            }

            //stop measuring audio download time
            Timers.StopTimer(Timers.Generic.SubtitlesDownloadTimer);

            //default
            return @"";
        }
    }
}
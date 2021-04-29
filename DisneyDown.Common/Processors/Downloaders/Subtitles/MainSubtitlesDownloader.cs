using DisneyDown.Common.Globals;
using DisneyDown.Common.Parsers;
using DisneyDown.Common.Util;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
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
                        //log existing status
                        ConsoleWriters.WriteLine($@"[i] Using existing {subtitlesMergeFile}; download skipped", ConsoleColor.Cyan);

                        //return existing subtitle merge file location
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
                            ConsoleWriters.WriteLine($@"[i] Found subtitles manifest: {subtitlePlaylistPathUrl}", ConsoleColor.Green);

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var subtitlePlaylistFullUrl = $"{masterBaseUri}{subtitlePlaylistPathUrl}";

                            //log the download
                            ConsoleWriters.WriteLine(@"[i] Downloading subtitles manifest", ConsoleColor.Cyan);

                            //do the download
                            var subtitlesManifest = ManifestParsers.DownloadManifest(subtitlePlaylistFullUrl);

                            //download processor
                            return PerformDownload(subtitlesManifest, subtitlePlaylistFullUrl, subtitlesMergeFile);
                        }
                        else

                            //report error
                            ConsoleWriters.WriteLine(@"[!] Subtitles download failed; subtitles playlist URL was null", ConsoleColor.Red);
                    }
                    else

                        //report error; subtitles cannot be downloaded in exclusive mode
                        ConsoleWriters.WriteLine("\n[!] Subtitles download failed; subtitles cannot be downloaded in exclusive mode\n", ConsoleColor.Red);
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Subtitles download failed; master playlist does not conform", ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.WriteLine($@"[!] Subtitles download error: {ex.Message}", ConsoleColor.Red);
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
                    var subtitlesBaseUrl = Methods.GetBaseUrl(subtitlesManifestUrl);

                    //do download
                    var savedDirectory = SegmentHandlers.DownloadAllSubtitlesSegments(subtitlesManifest, subtitlesBaseUrl,
                        Verification.MainContent, subtitlesMergeFile,
                        @"[Subtitles]");

                    //report success
                    ConsoleWriters.WriteLine(
                        $"\n[i] Successfully downloaded subtitles data to: {savedDirectory}\n", ConsoleColor.Green);

                    //stop measuring audio download time
                    Timers.StopTimer(Timers.Generic.SubtitlesDownloadTimer);

                    //return the saved files directory
                    return savedDirectory;
                }
                else

                    //report error
                    ConsoleWriters.WriteLine(@"[!] Subtitles download failed; subtitles manifest does not conform", ConsoleColor.Red);
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
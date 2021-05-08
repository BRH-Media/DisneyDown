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
                        ConsoleWriters.ConsoleWriteInfo($@"Using existing {subtitlesMergeFile}; download skipped");

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
                            ConsoleWriters.ConsoleWriteSuccess($@"Found subtitles manifest: {subtitlePlaylistPathUrl}");

                            //create fully-qualified URL for the playlist
                            var masterBaseUri = Methods.GetBaseUrl(masterPlaylistUrl);
                            var subtitlePlaylistFullUrl = $"{masterBaseUri}{subtitlePlaylistPathUrl}";

                            //log the download
                            ConsoleWriters.ConsoleWriteInfo(@"Downloading subtitles manifest");

                            //do the download
                            var subtitlesManifest = ManifestParsers.DownloadManifest(subtitlePlaylistFullUrl);

                            //download processor
                            return PerformDownload(subtitlesManifest, subtitlePlaylistFullUrl, subtitlesMergeFile);
                        }
                        else

                            //report error
                            ConsoleWriters.ConsoleWriteError(@"Subtitles download failed; subtitles playlist URL was null");
                    }
                    else

                        //report error; subtitles cannot be downloaded in exclusive mode
                        ConsoleWriters.ConsoleWriteError("Subtitles download failed; subtitles cannot be downloaded in exclusive mode\n");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Subtitles download failed; master playlist does not conform");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Subtitles download error: {ex.Message}");
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
                    ConsoleWriters.ConsoleWriteSuccess(
                        $"Successfully downloaded subtitles data to: {savedDirectory}\n");

                    //stop measuring audio download time
                    Timers.StopTimer(Timers.Generic.SubtitlesDownloadTimer);

                    //return the saved files directory
                    return savedDirectory;
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Subtitles download failed; subtitles manifest does not conform");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Subtitles download error: {ex.Message}");
            }

            //stop measuring audio download time
            Timers.StopTimer(Timers.Generic.SubtitlesDownloadTimer);

            //default
            return @"";
        }
    }
}
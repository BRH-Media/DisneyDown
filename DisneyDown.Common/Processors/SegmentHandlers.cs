using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers.HLS;
using DisneyDown.Common.Parsers.HLS.Playlist;
using DisneyDown.Common.Util.Diagnostics;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable UnusedMember.Global
// ReSharper disable LocalizableElement
// ReSharper disable InvertIf

namespace DisneyDown.Common.Processors
{
    /// <summary>
    /// Contains methods for handling segment manipulation and ordering
    /// </summary>
    public static class SegmentHandlers
    {
        /// <summary>
        /// Writes a segment to a file; creates the file if it doesn't exist, otherwise it will append.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        /// <param name="append"></param>
        public static void WriteSegment(string path, byte[] bytes, bool append = true)
        {
            //null validation
            if (bytes != null)

                //if the file exists (and we're allowed to append to it)
                if (File.Exists(path) && append)

                    //append to the already existing file instead of overwriting it
                    AppendAllBytes(path, bytes);
                else

                    //create the new file/overwrite the existing file
                    File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Writes a segment to a file; creates the file if it doesn't exist, otherwise it will append.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="append"></param>
        public static void WriteSegment(string path, string text, bool append = true)
        {
            //null validation
            if (!string.IsNullOrWhiteSpace(text))
            {
                //text to bytes
                var bytes = Encoding.Default.GetBytes(text);

                //pass on to byte handler
                WriteSegment(path, bytes, append);
            }
        }

        /// <summary>
        /// Append bytes to an existing file; will fail if the file does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public static void AppendAllBytes(string path, byte[] bytes)
        {
            //the file to append to must exist,
            //and the supplied contents must not be null
            if (File.Exists(path) && bytes != null)

                //open a new FileStream in append (a) mode
                using (var stream = new FileStream(path, FileMode.Append))

                    //write all bytes to the end of the file
                    stream.Write(bytes, 0, bytes.Length);
        }

        private static List<PlaylistUriItem> FilterUrlItems(IReadOnlyCollection<PlaylistItem> items, string filter)
        {
            try
            {
                //ensure we were provided with valid items
                if (items != null && !string.IsNullOrWhiteSpace(filter))
                {
                    //store all filtered segments here
                    var segments = new List<PlaylistUriItem>();

                    //debugging output
                    ConsoleWriters.ConsoleWriteDebug($"Filtering {items.Count} segments based on filter '{filter}'");

                    //go through each item and perform the filter
                    foreach (var s in items)

                        try
                        {
                            //try cast to URI
                            var uri = ((PlaylistUriItem)s).Uri;

                            //ensure the cast URI is valid
                            if (!string.IsNullOrWhiteSpace(uri))
                            {

                                //ensure a valid match
                                if (uri.Contains(filter))
                                {

                                    //add it to the list of valid segments
                                    segments.Add((PlaylistUriItem)s);
                                }
                                else
                                {
                                    //debugging output
                                    ConsoleWriters.ConsoleWriteDebug($"Filter failed: {uri}");
                                }
                            }
                            else
                            {
                                //debugging output
                                ConsoleWriters.ConsoleWriteDebug(@"Filter failed: empty string is not a URL");
                            }
                        }
                        catch (InvalidCastException)
                        {
                            //do nothing
                        }

                    //debugging output
                    ConsoleWriters.ConsoleWriteDebug($"Segment filter process completed ({segments.Count}/{items.Count})");

                    //return the filled list
                    return segments;
                }
                else
                {
                    //alert the user
                    ConsoleWriters.ConsoleWriteError(@"Could not filter HLS segments: null parse result");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Playlist segment filter error: {ex.Message}");
            }

            //alert user
            ConsoleWriters.ConsoleWriteWarning(@"Returned null filter list: no segments will be processed");

            //default
            return new List<PlaylistUriItem>();
        }

        /// <summary>
        /// Download all valid MPEG-4 segments in a manifest file (audio or video), then append them to a respective singular Widevine-protected file.
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="baseUri"></param>
        /// <param name="filePath"></param>
        /// <param name="correctUrlComponent"></param>
        /// <param name="displayPrefix"></param>
        public static void DownloadAllMpegSegments(string playlist, string baseUri, string correctUrlComponent, string filePath = @"segments.bin", string displayPrefix = @"")
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //validation
                    if (p != null)
                    {
                        //current item counter
                        var counter = 0;

                        //filter out any unnecessary segments (only get the MAIN segments, for example)
                        var filteredSegments = FilterUrlItems(p.Items, correctUrlComponent);

                        //total amount of segments
                        var totalSegments = filteredSegments.Count;

                        //report merge file
                        ConsoleWriters.ConsoleWriteDebug($"Discovered segments: {totalSegments} (Filtered: {p.Items.Count} - '{correctUrlComponent}')");
                        ConsoleWriters.ConsoleWriteInfo($"Starting segment download on merge file: {filePath}\n");

                        //go through each item in the playlist
                        foreach (var i in filteredSegments)
                        {
                            try
                            {
                                //fully-qualified segment URL
                                var segmentUrl = $"{baseUri}{i.Uri}";

                                //try download of segment
                                var segment = ResourceGrab.GrabBytes(segmentUrl);

                                //segment file name from URL
                                var segmentFileName = Path.GetFileName(new Uri(segmentUrl).LocalPath);

                                //% completion
                                var progress = decimal.Divide(counter + 1, totalSegments);

                                //validation
                                if (segment != null)
                                {
                                    //flush to file
                                    WriteSegment(filePath, segment);

                                    //report success
                                    ConsoleWriters.ConsoleWriteSuccess(
                                        $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) downloaded and merged | {progress:P2}");
                                }
                                else

                                    //report failure
                                    ConsoleWriters.ConsoleWriteSuccess(
                                        $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) download error: null result | {progress:P2}");

                                //incremented only on valid segment URL to keep count fluid
                                counter++;
                            }
                            catch (Exception ex)
                            {
                                //report error
                                ConsoleWriters.ConsoleWriteError(
                                    $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                    $"Segment {counter + 1:D4}/{totalSegments:D4} download error: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Could not download playlist: playlist was not correctly parsed");
                    }
                }
                else
                {
                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Could not download playlist: null playlist was provided");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"MPEG-4 playlist download error: {ex.Message}");
            }
        }

        /// <summary>
        /// Download all valid subtitles segments in a manifest file, and return the directory where they are located
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="baseUri"></param>
        /// <param name="filePath"></param>
        /// <param name="correctUrlComponent"></param>
        /// <param name="displayPrefix"></param>
        public static string DownloadAllSubtitlesSegments(string playlist, string baseUri, string correctUrlComponent, string filePath = @"subtitles.srt", string displayPrefix = @"")
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //validation
                    if (p != null)
                    {
                        //current item counter
                        var counter = 0;

                        //filter out any unnecessary segments (only get the MAIN segments, for example)
                        var filteredSegments = FilterUrlItems(p.Items, correctUrlComponent);

                        //total amount of segments
                        var totalSegments = filteredSegments.Count;

                        //WebVTT merge file
                        var segmentMergeFile = $@"{Path.GetDirectoryName(filePath)}\{Path.GetFileNameWithoutExtension(filePath)}.vtm";

                        //report merge file
                        ConsoleWriters.ConsoleWriteInfo($"Starting subtitle download on merge file: {filePath}\n");

                        //go through each item in the playlist
                        foreach (var i in filteredSegments)
                        {
                            try
                            {
                                //fully-qualified segment URL
                                var segmentUrl = $"{baseUri}{i.Uri}";

                                //try download of segment
                                var segment = ResourceGrab.GrabBytes(segmentUrl);

                                //segment file name from URL
                                var segmentFileName = Path.GetFileName(new Uri(segmentUrl).LocalPath);

                                //% completion
                                var progress = decimal.Divide(counter + 1, totalSegments);

                                //validation
                                if (segment != null)
                                {
                                    //flush to file
                                    WriteSegment(segmentMergeFile, segment);

                                    //report success
                                    ConsoleWriters.ConsoleWriteSuccess(
                                        $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) downloaded and merged | {progress:P2}");
                                }
                                else

                                    //report failure
                                    ConsoleWriters.ConsoleWriteSuccess(
                                        $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) download error: null result | {progress:P2}");

                                //incremented only on valid segment URL to keep count fluid
                                counter++;
                            }
                            catch (Exception ex)
                            {
                                //report error
                                ConsoleWriters.ConsoleWriteError(
                                    $"{(!string.IsNullOrWhiteSpace(displayPrefix) ? $"{displayPrefix} " : @"")}" +
                                    $"Segment {counter + 1:D4}/{totalSegments:D4} download error: {ex.Message}");
                            }
                        }

                        //report progress
                        ConsoleWriters.ConsoleWriteInfo(@"Attempting subtitles parse and merge");

                        //start measuring subtitles parse and merge time
                        Timers.StartTimer(Timers.Generic.SubtitlesParseTimer);

                        //total lines
                        var subtitles = File.ReadAllLines(segmentMergeFile);

                        //check if there are lines
                        if (subtitles.Length > 0)
                        {
                            //conversion
                            var convertedSubs = SubtitleProcessor.ConvertToSrt(subtitles.ToList());

                            //export to the merge file
                            File.WriteAllLines(filePath, convertedSubs);
                        }

                        //stop measuring subtitle parse and merge time
                        Timers.StopTimer(Timers.Generic.SubtitlesParseTimer);

                        //return the directory of the saved subtitle segments
                        return filePath;
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Subtitles playlist download error: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}
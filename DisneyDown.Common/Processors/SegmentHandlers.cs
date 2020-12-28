using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers.HLS;
using DisneyDown.Common.Parsers.HLS.Playlist;
using System;
using System.Collections.Generic;
using System.IO;

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
        public static void WriteSegment(string path, byte[] bytes)
        {
            if (bytes != null)

                if (File.Exists(path))
                    AppendAllBytes(path, bytes);
                else
                    File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// Append bytes to an existing file; will fail if the file does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public static void AppendAllBytes(string path, byte[] bytes)
        {
            if (File.Exists(path) && bytes != null)

                using (var stream = new FileStream(path, FileMode.Append))
                    stream.Write(bytes, 0, bytes.Length);
        }

        private static List<PlaylistUriItem> FilterUrlItems(List<PlaylistItem> items, string filter)
        {
            try
            {
                //ensure we were provided with valid items
                if (items != null && !string.IsNullOrWhiteSpace(filter))
                {
                    //store all filtered segments here
                    var segments = new List<PlaylistUriItem>();

                    //go through each item and perform the filter
                    foreach (var s in items)

                        try
                        {
                            //try cast to URI
                            var uri = ((PlaylistUriItem)s).Uri;

                            //ensure the cast URI is valid
                            if (!string.IsNullOrWhiteSpace(uri))

                                //ensure a valid match
                                if (uri.Contains(filter))

                                    //add it to the list of valid segments
                                    segments.Add((PlaylistUriItem)s);
                        }
                        catch (InvalidCastException)
                        {
                            //nothing
                        }

                    //return the filled list
                    return segments;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Playlist segment filter error: {ex.Message}");
            }

            //default
            return new List<PlaylistUriItem>();
        }

        /// <summary>
        /// Download all valid segments in a manifest file (audio or video), then append them to a respective singular Widevine-protected file.
        /// </summary>
        /// <param name="playlist"></param>
        /// <param name="baseUri"></param>
        /// <param name="filePath"></param>
        /// <param name="correctUrlComponent"></param>
        public static void DownloadAllSegments(string playlist, string baseUri, string correctUrlComponent, string filePath = @"segments.bin")
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
                        Console.WriteLine($"\nStarting segment download on merge file: {filePath}\n");

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
                                    Console.WriteLine(
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) downloaded and merged | {progress:P2}");
                                }
                                else

                                    //report failure
                                    Console.WriteLine(
                                        $"Segment {counter + 1:D4}/{totalSegments:D4} ({segmentFileName}) download error: null result | {progress:P2}");

                                //incremented only on valid segment URL to keep count fluid
                                counter++;
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Segment {counter + 1:D4}/{totalSegments:D4} download error: {ex.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Playlist download error:\n\n{ex.Message}");
            }
        }
    }
}
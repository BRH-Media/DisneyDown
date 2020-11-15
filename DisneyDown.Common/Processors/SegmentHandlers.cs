﻿using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers.HLSParser;
using DisneyDown.Common.Processors.Parsers.HLSParser.Playlist;
using System;
using System.IO;

namespace DisneyDown.Common.Processors
{
    public static class SegmentHandlers
    {
        public static void WriteSegment(string path, byte[] bytes)
        {
            if (bytes != null)
            {
                if (File.Exists(path))
                    AppendAllBytes(path, bytes);
                else
                    File.WriteAllBytes(path, bytes);
            }
        }

        public static void AppendAllBytes(string path, byte[] bytes)
        {
            if (File.Exists(path) && bytes != null)
            {
                using (var stream = new FileStream(path, FileMode.Append))
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public static void DownloadAllSegments(string playlist, string baseUri, string filePath = @"segments.bin", string correctUrlComponent = @"-MAIN/")
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

                        //report merge file
                        Console.WriteLine($"Starting segment download on merge file: {filePath}\n");

                        //go through each item in the playlist
                        foreach (var i in p.Items)
                        {
                            try
                            {
                                //try cast to URI
                                var uri = ((PlaylistUriItem)i).Uri;

                                //validation
                                if (!string.IsNullOrWhiteSpace(uri))
                                {
                                    //fully-qualified segment URL
                                    var segmentUrl = $"{baseUri}{uri}";

                                    //validate it using the 'MAIN' audio component check
                                    if (segmentUrl.Contains(correctUrlComponent))
                                    {
                                        //segment file name from URL
                                        var segmentFileName = Path.GetFileName(new Uri(segmentUrl).LocalPath);

                                        //try download of segment
                                        var segment = ResourceGrab.GrabBytes(segmentUrl);

                                        //validation
                                        if (segment != null)
                                        {
                                            //flush to file
                                            WriteSegment(filePath, segment);

                                            Console.WriteLine(
                                                $"Segment {counter} ({segmentFileName}) downloaded and merged");
                                        }
                                        else
                                            Console.WriteLine(
                                                $"Segment {counter} ({segmentFileName}) download error: null result");

                                        //incremented only on valid segment URL to keep count fluid
                                        counter++;
                                    }
                                }
                            }
                            catch (InvalidCastException)
                            {
                                //nothing
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Segment {counter} download error: {ex.Message}");
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
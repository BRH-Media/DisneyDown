using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers.HLSParser;
using DisneyDown.Common.Processors.Parsers.HLSParser.Playlist;
using System;
using System.Collections.Generic;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Parsers
{
    public static class ManifestParsers
    {
        public static bool ManifestValid(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse
                    var p = new PlaylistParser().Parse(playlist);

                    //validation
                    if (p != null)
                    {
                        //additional validation
                        if (p.Items.Count > 0)
                        {
                            //get first tag
                            var tag = (PlaylistTagItem)p.Items[0];

                            //verify
                            return tag.Id == PlaylistTagId.EXTM3U;
                        }
                    }
                }
            }
            catch
            {
                //nothing
            }

            //default
            return false;
        }

        public static string DownloadManifest(string playlistUrl)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlistUrl))
                {
                    //try playlist download
                    var playlist = ResourceGrab.GrabString(playlistUrl);

                    //validation
                    if (!string.IsNullOrWhiteSpace(playlist))

                        //return downloaded playlist
                        return playlist;
                }
                else
                    Console.WriteLine($"Incorrect content/playlist URL: {playlistUrl}");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Playlist download error:\n\n{ex.Message}");
            }

            //default
            return @"";
        }

        public static string ManifestMapUrl(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //attempt load
                    var p = new PlaylistParser().Parse(playlist);

                    //validation
                    if (p != null)
                    {
                        //playlist maps are stored temporarily
                        var maps = new List<string>();

                        //go through each located tag
                        foreach (var i in p.Items)
                        {
                            try
                            {
                                //try casting to tag
                                var tag = (PlaylistTagItem)i;

                                //verify map
                                if (tag.Id == PlaylistTagId.EXT_X_MAP)
                                    maps.Add(tag.Attributes[0].Value);
                            }
                            catch
                            {
                                //ignore
                            }
                        }

                        //Disney+ has a "BUMPER" section that is purely the intro, so we attempt to grab the second one if it exists
                        return maps.Count > 1 ? maps[1] : maps[0];
                    }
                    else
                        Console.WriteLine(@"Null playlist parse result; couldn't find map URL");
                }
                else
                    Console.WriteLine(@"Null or empty playlist supplied; couldn't find map URL");
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Playlist parse error:\n\n{ex.Message}");
            }

            //default
            return @"";
        }
    }
}
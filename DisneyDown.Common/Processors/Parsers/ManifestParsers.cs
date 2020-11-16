using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers.HLSParser;
using DisneyDown.Common.Processors.Parsers.HLSParser.Playlist;
using System;
using System.Linq;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Processors.Parsers
{
    /// <summary>
    /// Contains generic methods for parsing content manifests
    /// </summary>
    public static class ManifestParsers
    {
        /// <summary>
        /// Verify if a manifest (master or content) is valid and conforms to the standard
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Download a manifest from a URL (contains additional verification than simply using ResourceGrab)
        /// </summary>
        /// <param name="playlistUrl"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Fetches the manifest MPEG-4 map URL (MPEG-4 initialisation segment data)
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string ManifestMapUrl(string playlist)
        {
            //check value for verification
            const string checkValue = @"-MAIN/";

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
                        var map = @"";

                        //go through each located tag
                        foreach (var i in p.Items)
                        {
                            try
                            {
                                //try casting to tag
                                var tag = (PlaylistTagItem)i;

                                //verify map
                                if (tag.Id == PlaylistTagId.EXT_X_MAP)
                                    //find URI attribute and verify it against the match criteria
                                    foreach (var a in tag.Attributes.Where(a
                                        => a.Key == @"URI" && a.Value.Contains(checkValue)))
                                    {
                                        //assign the return value
                                        map = a.Value;
                                        break;
                                    }
                            }
                            catch
                            {
                                //ignore
                            }
                        }

                        //return the result
                        return map;
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
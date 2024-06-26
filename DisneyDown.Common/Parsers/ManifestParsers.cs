﻿using DisneyDown.Common.Globals;
using DisneyDown.Common.Net;
using DisneyDown.Common.Parsers.HLS;
using DisneyDown.Common.Parsers.HLS.Playlist;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InvertIf
// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.Parsers
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
                //null validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse
                    var p = new PlaylistParser().Parse(playlist);

                    //null validation
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            //default
            return false;
        }

        /// <summary>
        /// Verify if a manifest is a valid master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static bool MasterValid(string playlist)
        {
            try
            {
                //null validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse
                    var p = new PlaylistParser().Parse(playlist);

                    //null validation
                    if (p != null)
                    {
                        //additional validation
                        if (p.Items.Count > 0)
                        {
                            //check if any items are PlaylistUrl
                            foreach (var item in p.Items)
                            {
                                try
                                {
                                    //try casting
                                    var playlistUri = (PlaylistUriItem)item;

                                    //validation
                                    if (playlistUri != null)
                                    {
                                        //uri
                                        var uri = playlistUri.Uri.Split('?')[0];

                                        //is it an m3u8?
                                        var master = uri.EndsWith(@".m3u8") || uri.EndsWith(@".m3u");

                                        //verification
                                        if (master)
                                        {
                                            //apply master parameter
                                            p.IsMaster = true;

                                            //exit loop
                                            break;
                                        }
                                    }
                                }
                                catch (InvalidCastException)
                                {
                                    //nothing
                                }
                            }

                            //return final result
                            return p.IsMaster;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Master validation error: {ex.Message}");
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
                    ConsoleWriters.ConsoleWriteError($"Incorrect content/playlist URL: {playlistUrl}");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist download error: {ex.Message}");
            }

            //default
            return @"";
        }

        private static bool ValidSegmentUrl(string urlSegment)

            =>  //if there's any match, it's an instant fail
                new[] { Verification.SegmentFilter.AuxiliaryContent.BumperIntro, Verification.SegmentFilter.AuxiliaryContent.DubCard }
                    .All(s => (!(urlSegment.Contains(s.PrimaryFilter.FilterString) && s.PrimaryFilter.Enabled))
                              && (!(urlSegment.Contains(s.SecondaryFilter.FilterString) && s.SecondaryFilter.Enabled))
                              && (!(urlSegment.Contains(s.FallbackFilter.FilterString) && s.FallbackFilter.Enabled)));

        public static byte[] ManifestRetrieveWidevinePssh(string playlist, string[][] schemes = null)
        {
            try
            {
                if (schemes == null)
                {
                    schemes = ManifestAllDrmSchemes(playlist);
                }

                if (schemes?.Length > 0)
                {
                    //go through each scheme until we find Widevine
                    foreach (var s in schemes)
                    {
                        if (s[0].Replace("-", "").Contains(DrmSchemeValues.WidevineUuid.ToString().Replace("-", "")))
                        {
                            //parse out the pssh box
                            var boxSplit = s[1].Split(',');
                            var box = boxSplit.Length > 1 ? boxSplit[1] : null;
                            if (!string.IsNullOrWhiteSpace(box))
                            {
                                //we should now have the base64-encoded PSSH box
                                var psshBytes = Convert.FromBase64String(box);

                                //validation
                                if (psshBytes.Length > 0)
                                {
                                    return psshBytes;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleWriters.ConsoleWriteError($"Failed to retrieve Widevine PSSH: {ex}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Lists all manifest tags that match the filter
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string[][] ManifestAllDrmSchemes(string playlist)
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
                        //playlist schemes are stored temporarily
                        var schemeList = new List<string[]>();

                        //go through each located tag
                        foreach (var i in p.Items)
                        {
                            try
                            {
                                //try casting to tag
                                var tag = (PlaylistTagItem)i;

                                //verify map
                                if (tag.Id == PlaylistTagId.EXT_X_SESSION_KEY)
                                {
                                    //two parts; 0 shall be the scheme identifier and 1 shall be the URI
                                    var scheme = new string[2];

                                    //find URI attribute and verify it against the match criteria
                                    foreach (var a in tag.Attributes)
                                    {
                                        if (a.Key == @"KEYFORMAT")
                                        {
                                            scheme[0] = a.Value;
                                        }
                                        else if (a.Key == @"URI")
                                        {
                                            scheme[1] = a.Value;
                                        }
                                    }

                                    //append accordingly
                                    schemeList.Add(scheme);
                                }
                            }
                            catch
                            {
                                //ignore
                            }
                        }

                        //return the result
                        return schemeList.ToArray();
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Null playlist parse result; couldn't find DRM schemes");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Null or empty playlist supplied; couldn't find DRM schemes");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist parse error: {ex.Message}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Lists all manifest MPEG-4 map URLs (MPEG-4 initialisation segment data)
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string[] ManifestAllMapUrls(string playlist)
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
                        var mapList = new List<string>();

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
                                        => a.Key == @"URI"))
                                    {
                                        //assign the return value
                                        mapList.Add(a.Value);
                                        break;
                                    }
                            }
                            catch
                            {
                                //ignore
                            }
                        }

                        //return the result
                        return mapList.ToArray();
                    }
                    else

                        //report error
                        ConsoleWriters.ConsoleWriteError(@"Null playlist parse result; couldn't find map URL");
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Null or empty playlist supplied; couldn't find map URL");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist parse error: {ex.Message}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Fetches the manifest MPEG-4 map URL (MPEG-4 initialisation segment data)
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string ManifestMainMapUrl(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //get all map urls
                    var mapList = ManifestAllMapUrls(playlist);

                    //loop through each one and return the first correct match
                    foreach (var m in mapList)

                        //validate the URL
                        if (ValidSegmentUrl(m))

                            //return result if valid
                            return m;
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Null or empty playlist supplied; couldn't find map URL");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist parse error: {ex.Message}");
            }

            //default
            return @"";
        }

        /// <summary>
        /// Fetches the manifest MPEG-4 Disney+ intro (bumper) map URL (MPEG-4 initialisation segment data)
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string ManifestBumperMapUrl(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //get all map urls
                    var mapList = ManifestAllMapUrls(playlist);

                    //loop through each one and return the first correct match
                    foreach (var m in mapList)

                        //validate the URL
                        if ((m.Contains(Verification.SegmentFilter.AuxiliaryContent.BumperIntro.PrimaryFilter.FilterString) && Verification.SegmentFilter.AuxiliaryContent.BumperIntro.PrimaryFilter.Enabled)
                            || (m.Contains(Verification.SegmentFilter.AuxiliaryContent.BumperIntro.SecondaryFilter.FilterString) && Verification.SegmentFilter.AuxiliaryContent.BumperIntro.SecondaryFilter.Enabled)
                            || (m.Contains(Verification.SegmentFilter.AuxiliaryContent.BumperIntro.FallbackFilter.FilterString) && Verification.SegmentFilter.AuxiliaryContent.BumperIntro.FallbackFilter.Enabled))

                            //return result if valid
                            return m;
                }
                else

                    //report error
                    ConsoleWriters.ConsoleWriteError(@"Null or empty playlist supplied; couldn't find Disney+ intro map URL");
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist parse error: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}
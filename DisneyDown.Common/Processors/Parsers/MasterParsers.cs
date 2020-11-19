using DisneyDown.Common.Net;
using DisneyDown.Common.Processors.Parsers.HLSParser;
using DisneyDown.Common.Processors.Parsers.HLSParser.Playlist;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DisneyDown.Common.Processors.Parsers
{
    /// <summary>
    /// Contains generic methods for parsing and handling master manifests from Disney+
    /// </summary>
    public static class MasterParsers
    {
        public static string[] FilterLanguages(PlaylistTagItem[] playlists)
        {
            //default language
            const string defaultLang = @"en";
            const string langPriorityFile = @"langPriority.cfg";

            try
            {
                //priority storage
                var langPriority = new List<string>()
                {
                    defaultLang
                };

                //does the lang priority file exist?
                if (File.Exists(langPriorityFile))
                {
                    //read the file into memory as text
                    var tmpLangPriority = File.ReadAllText(langPriorityFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(tmpLangPriority))
                    {
                        //read the file into memory as lines
                        langPriority = File.ReadAllLines(langPriorityFile).ToList();
                    }
                    else
                        //overwrite it
                        File.WriteAllText(langPriorityFile, defaultLang);
                }
                else
                    //create it
                    File.WriteAllText(langPriorityFile, defaultLang);

                //store all playlist URLs here
                var playlistUrls = new List<string>();

                foreach (var l in langPriority)
                {
                    foreach (var t in playlists)
                    {
                        //temporary validation variable
                        var langValid = false;

                        //loop through and parse attributes
                        foreach (var a in t.Attributes)
                        {
                            switch (a.Key)
                            {
                                //validate the attribute
                                case @"LANGUAGE" when string.Equals(a.Value, l, StringComparison.CurrentCultureIgnoreCase):
                                    langValid = true;
                                    break;

                                case @"URI" when langValid:
                                    playlistUrls.Add(a.Value);
                                    break;
                            }
                        }
                    }

                    //language priority found, no need to keep running
                    if (playlistUrls.Count > 0)
                        break;
                }

                //return the new list
                return playlistUrls.ToArray();
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Language priority error: {ex.Message}. Will use '{defaultLang}'.");
            }

            //default
            return new string[] { };
        }

        /// <summary>
        /// Verifies if an audio tag posses the correct attributes
        /// </summary>
        /// <param name="audioTrack"></param>
        /// <returns></returns>
        public static bool ValidAudioTrack(PlaylistTagItem audioTrack)
        {
            //tag constants
            const string audioType = @"AUDIO";

            try
            {
                //validation
                if (audioTrack != null)
                {
                    //validation
                    if (audioTrack.Attributes.Count > 0
                        && audioTrack.Id == PlaylistTagId.EXT_X_MEDIA)
                    {
                        //final return values
                        var typeValid = false;

                        //loop through and parse attributes
                        foreach (var a in audioTrack.Attributes)
                        {
                            switch (a.Key)
                            {
                                //validate the attribute
                                case @"TYPE" when a.Value == audioType:
                                    typeValid = true;
                                    break;
                            }
                        }

                        //verify
                        return typeValid;
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
        /// Sorts the best quality playlist from a list of playlist URLs
        /// </summary>
        /// <param name="playlistUrls">List of URLs</param>
        /// <param name="definitions">Use the BandwidthDefinitions class to handle this argument</param>
        /// <returns></returns>
        public static string SortBestPlaylist(string[] playlistUrls, Dictionary<int, string> definitions)
        {
            try
            {
                //validate
                if (playlistUrls != null)
                {
                    //here we assign each URL an integer which describes its quality level from 0-6
                    var qualityDict = new List<Tuple<int, string>>();

                    //loop through each URL
                    foreach (var url in playlistUrls)
                    {
                        //validation
                        if (string.IsNullOrWhiteSpace(url)) continue;

                        //the matched quality
                        var q = 0;

                        //try and match a quality level
                        foreach (var def in definitions)
                        {
                            //check the current quality against the URL
                            if (url.Contains(def.Value))
                            {
                                q = def.Key;
                                break;
                            }
                        }

                        //apply the quality to the list
                        qualityDict.Add(new Tuple<int, string>(q, url));
                    }

                    //get the first item that's of the highest quality rating
                    var maxQuality = qualityDict.OrderByDescending(x => x.Item1).First();

                    //return the result
                    return maxQuality.Item2;
                }
            }
            catch
            {
                //nothing
            }

            //default
            return @"";
        }

        /// <summary>
        /// Sort the best quality video playlist from a master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string MasterVideoPlaylist(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //store all playlist URIs here
                    var playlistUrls = new List<string>();

                    //loop through each track
                    foreach (var t in p.Items)
                    {
                        try
                        {
                            //try cast to uri
                            var uri = (PlaylistUriItem)t;

                            //validation
                            if (uri != null)
                            {
                                //add to the list if it's not already in there
                                if (!playlistUrls.Contains(uri.Uri))
                                    playlistUrls.Add(uri.Uri);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }

                    //return filtered result
                    return SortBestPlaylist(playlistUrls.ToArray(), BandwidthDefinitions.VideoDefinitions);
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Parse master error:\n\n{ex.Message}");
            }

            //default
            return @"";
        }

        /// <summary>
        /// Sort the best quality audio playlist from a master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string MasterAudioPlaylist(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //store all playlist URIs here
                    var playlistUrls = new List<string>();
                    var playlistTags = new List<PlaylistTagItem>();

                    //loop through each track
                    foreach (var t in p.Items)
                    {
                        try
                        {
                            //try cast to tag
                            var tag = (PlaylistTagItem)t;

                            //validation
                            if (tag != null)
                            {
                                //verify valid audio
                                var trackValid = ValidAudioTrack(tag);

                                //return the URI of the tag if it's valid
                                if (trackValid)
                                {
                                    //attempt to parse out the URI
                                    foreach (var a in tag.Attributes)
                                    {
                                        //verify if it's a URI tag
                                        if (a.Key == @"URI")
                                        {
                                            playlistUrls.Add(a.Value);
                                            playlistTags.Add(tag);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }

                    //filter out unwanted languages
                    var finalPlaylists = FilterLanguages(playlistTags.ToArray());

                    //return filtered result
                    return SortBestPlaylist(finalPlaylists, BandwidthDefinitions.AudioDefinitions);
                }
            }
            catch (Exception ex)
            {
                //report error
                Console.WriteLine($"Parse master error:\n\n{ex.Message}");
            }

            //default
            return @"";
        }
    }
}
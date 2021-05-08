using DisneyDown.Common.Globals;
using DisneyDown.Common.Parsers.HLS;
using DisneyDown.Common.Parsers.HLS.Playlist;
using DisneyDown.Common.Util.Kit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ReSharper disable LocalizableElement
// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable ForeachCanBeConvertedToQueryUsingAnotherGetEnumerator
// ReSharper disable InvertIf

namespace DisneyDown.Common.Parsers
{
    /// <summary>
    /// Contains generic methods for parsing and handling master manifests from Disney+
    /// </summary>
    public static class MasterParsers
    {
        public static string[] LangPriority(string langPriorityFile)
        {
            try
            {
                //priority storage
                var langPriority = new List<string>
                {
                    //ensure the default language is first in the stack to make sure it still gets selected
                    //if nothing is found in the priority file
                    Strings.DefaultLang
                };

                //does the lang priority file exist?
                if (File.Exists(langPriorityFile))
                {
                    //read the file into memory as text
                    var tmpLangPriority = File.ReadAllText(langPriorityFile);

                    //verification
                    if (!string.IsNullOrWhiteSpace(tmpLangPriority))

                        //read the file into memory as lines
                        langPriority = File.ReadAllLines(langPriorityFile).ToList();
                    else

                        //overwrite it
                        File.WriteAllText(langPriorityFile, Strings.DefaultLang);
                }
                else
                {
                    //directory of language priority file
                    var langDir = Path.GetDirectoryName(langPriorityFile);

                    //verify the language priority file directory value
                    if (!string.IsNullOrWhiteSpace(langDir))

                        //does the directory that it will sit in exist?
                        if (!Directory.Exists(langDir))

                            //it doesn't; create a new directory
                            Directory.CreateDirectory(langDir);

                    //create the new language priority file
                    File.WriteAllText(langPriorityFile, Strings.DefaultLang);
                }

                //return final priority
                return langPriority.ToArray();
            }
            catch (Exception ex)
            {
                //report error
                Console.Write($@"Language priority file read error: {ex.Message}");
            }

            //default
            return new string[] { };
        }

        public static string[] FilterLanguages(PlaylistTagItem[] playlistTags, string langPriorityFile)
        {
            try
            {
                //store all playlist URLs here
                var playlistUrls = new List<string>();

                //fetch priority from file
                var langPriority = LangPriority(langPriorityFile);

                //each line in the language priority file
                foreach (var l in langPriority)
                {
                    //line verification
                    if (!string.IsNullOrWhiteSpace(l))
                    {
                        //options
                        var langOptions = l.Split(':');

                        //there must be at least one entry in the language priority options
                        if (langOptions.Length > 0)
                        {
                            //language is first entry
                            var langCode = langOptions[0];

                            //language subtitle forced is second entry
                            var langForced = langOptions.Length > 1
                                ? langOptions[1]
                                : @"";

                            //go through each provided playlist tag element
                            foreach (var t in playlistTags)
                            {
                                //temporary validation variable
                                var langValid = false;

                                //loop through and parse attributes
                                foreach (var a in t.Attributes)
                                {
                                    //check what key matches; actions will vary depending
                                    switch (a.Key)
                                    {
                                        //validate the "LANGUAGE" code attribute (e.g. "en" or "de")
                                        case @"LANGUAGE" when

                                            //was a "LANGUAGE" code attribute supplied?
                                            !string.IsNullOrWhiteSpace(langCode) &&

                                            //and in addition, does the "LANGUAGE" code attribute match that of the current tag?
                                            string.Equals(langCode, a.Value,
                                                StringComparison.CurrentCultureIgnoreCase):

                                            //enable language valid since the correct language code was matched
                                            langValid = true;

                                            //exit conditional
                                            break;

                                        //validate the subtitle forced attribute (e.g. "YES" or "NO")
                                        case @"FORCED" when

                                            //was a "FORCED" attribute supplied?
                                            !string.IsNullOrWhiteSpace(langForced) &&

                                            //and in addition, does the "FORCED" attribute not match that of the current tag?
                                            !string.Equals(langForced, a.Value,
                                                StringComparison.CurrentCultureIgnoreCase):

                                            //disable language valid, since an incorrect subtitle force value was encountered
                                            langValid = false;

                                            //exit conditional
                                            break;

                                        //only add the URI if the language priority has been reached
                                        case @"URI" when

                                            //master condition
                                            langValid:

                                            //add it to the total playlists list; all prior conditions satisfied
                                            playlistUrls.Add(a.Value);

                                            //exit conditional
                                            break;
                                    }
                                }
                            }

                            //language priority found, no need to keep running
                            if (playlistUrls.Count > 0)
                                break;
                        }
                    }
                }

                //return the new list
                return playlistUrls.ToArray();
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($@"Language priority error: {ex.Message}. Will use '{Strings.DefaultLang}'.");
            }

            //default
            return new string[] { };
        }

        /// <summary>
        /// Verifies a track against its ID string
        /// </summary>
        /// <param name="track">Track to verify</param>
        /// <param name="correctTagType">ID string</param>
        /// <returns></returns>
        public static bool ValidTrack(PlaylistTagItem track, string correctTagType)
        {
            try
            {
                //validation
                if (track != null)

                    //validation
                    if (track.Attributes.Count > 0
                        && track.Id == PlaylistTagId.EXT_X_MEDIA)
                    {
                        //final return values
                        var typeValid = false;

                        //loop through and parse attributes
                        foreach (var a in track.Attributes)

                            //case the type information
                            switch (a.Key)
                            {
                                //validate the attribute
                                case @"TYPE" when a.Value == correctTagType:
                                    typeValid = true;
                                    break;
                            }

                        //verify
                        return typeValid;
                    }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Track validation error: {ex.Message}");
            }

            //default
            return false;
        }

        /// <summary>
        /// Verifies if an audio tag possesses the correct attributes
        /// </summary>
        /// <param name="audioTrack"></param>
        /// <returns></returns>
        public static bool ValidAudioTrack(PlaylistTagItem audioTrack)
            => ValidTrack(audioTrack, @"AUDIO");

        /// <summary>
        /// Verifies if a subtitle tag possesses the correct attributes
        /// </summary>
        /// <param name="subtitleTrack"></param>
        /// <returns></returns>
        public static bool ValidSubtitleTrack(PlaylistTagItem subtitleTrack)
            => ValidTrack(subtitleTrack, @"SUBTITLES");

        /// <summary>
        /// Sorts the best quality playlist from a list of playlist URLs
        /// </summary>
        /// <param name="playlistUrls">List of URLs</param>
        /// <param name="definitions">Use the BandwidthDefinitions class to handle this argument</param>
        /// <returns></returns>
        public static Tuple<string, QualityRating> SortBestPlaylist(string[] playlistUrls, Dictionary<int, QualityRating> definitions)
        {
            try
            {
                //validate
                if (playlistUrls != null)
                {
                    //here we assign each URL an integer which describes its quality level
                    var qualityDict = new List<Tuple<int, string>>();

                    //loop through each URL
                    foreach (var url in playlistUrls)
                    {
                        //validation
                        if (string.IsNullOrWhiteSpace(url))
                            continue;

                        //the matched quality index
                        var qualityIndex = 0;

                        //try and match a quality level
                        foreach (var def in definitions)

                            //check the current quality against the URL
                            if (url.Contains(def.Value.SearchCriteria))
                            {
                                //index of the current matched quality
                                qualityIndex = def.Key;
                                break;
                            }

                        //apply the quality to the list
                        qualityDict.Add(new Tuple<int, string>(qualityIndex, url));
                    }

                    //get the first item that's of the highest quality rating
                    var (qualityLevel, matchedUrl) = qualityDict.OrderByDescending(x => x.Item1).First();

                    //return the result
                    return new Tuple<string, QualityRating>(matchedUrl, definitions[qualityLevel]);
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Playlist sorting error: {ex.Message}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Sort the best quality video playlist from a master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static Tuple<string, QualityRating> MasterVideoPlaylistUri(string playlist)
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

                                //add to the list if it's not already in there
                                if (!playlistUrls.Contains(uri.Uri))
                                    playlistUrls.Add(uri.Uri);
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
                ConsoleWriters.ConsoleWriteError($"Parse master video URI error: {ex.Message}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Sort the best quality audio playlist from a master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static Tuple<string, QualityRating> MasterAudioPlaylistUri(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //store all playlist URIs here
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

                                    //attempt to parse out the URI
                                    foreach (var a in tag.Attributes)

                                        //verify if it's a URI tag
                                        if (a.Key == @"URI")

                                            //store the verified URI tag
                                            playlistTags.Add(tag);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }

                    //filter out unwanted languages
                    var finalPlaylists = FilterLanguages(playlistTags.ToArray(), Strings.AudioLangPriorityFile);

                    //return filtered result
                    return SortBestPlaylist(finalPlaylists, BandwidthDefinitions.AudioDefinitions);
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Parse master audio URI error: {ex.Message}");
            }

            //default
            return null;
        }

        /// <summary>
        /// Sort the highest priority subtitle playlist from a master manifest
        /// </summary>
        /// <param name="playlist"></param>
        /// <returns></returns>
        public static string MasterSubtitleUrl(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //store all playlist URIs here
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
                                //verify valid subtitle
                                var trackValid = ValidSubtitleTrack(tag);

                                //return the URI of the tag if it's valid
                                if (trackValid)

                                    //attempt to parse out the URI
                                    foreach (var a in tag.Attributes)

                                        //verify if it's a URI tag
                                        if (a.Key == @"URI")

                                            //store the verified URI tag
                                            playlistTags.Add(tag);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }

                    //filter out unwanted languages
                    var finalPlaylists = FilterLanguages(playlistTags.ToArray(), Strings.SubtitleLangPriorityFile);

                    //return first filtered result by default
                    return finalPlaylists.Length > 0
                        ? finalPlaylists[0]
                        : @"";
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Parse master subtitle URI error: {ex.Message}");
            }

            //default
            return @"";
        }
    }
}
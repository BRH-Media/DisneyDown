using DisneyDown.Common.Interpreters.HLSParser;
using DisneyDown.Common.Interpreters.HLSParser.Playlist;
using DisneyDown.Common.Net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

// ReSharper disable LocalizableElement

namespace DisneyDown.UI
{
    public partial class Home : Form
    {
        public string GlobalPlaylist { get; set; } = @"";

        public Home()
        {
            InitializeComponent();
        }

        public bool ValidAudioTrack(PlaylistTagItem audioTrack)
        {
            //constants
            const string audioGroup = @"eac-3";
            const string audioName = @"English";
            const string audioType = @"AUDIO";

            try
            {
                //validation
                if (audioTrack != null)
                {
                    //validation
                    if (audioTrack.Attributes.Count > 0)
                    {
                        //final return values
                        var nameValid = false;
                        var groupValid = false;
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

                                case @"NAME" when a.Value == audioName:
                                    nameValid = true;
                                    break;

                                case @"GROUP-ID" when a.Value == audioGroup:
                                    groupValid = true;
                                    break;
                            }
                        }

                        //return valid
                        return nameValid && groupValid && typeValid;
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

        public string ParseBestVideoPlaylist(string[] playlistUrls)
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
                        foreach (var def in BandwidthDefinitions.Definitions)
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

        public string ParseMasterVideoPlaylist(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //store all playlist URIs here
                    var playlists = new List<string>();

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
                                if (!playlists.Contains(uri.Uri))
                                    playlists.Add(uri.Uri);
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }

                    //return filtered result
                    return ParseBestVideoPlaylist(playlists.ToArray());
                }
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Parse master error:\n\n{ex}");
            }

            //default
            return @"";
        }

        public string ParseMasterAudioPlaylist(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

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
                                            return a.Value;
                                    }
                                }
                            }
                        }
                        catch
                        {
                            //ignore
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Parse master error:\n\n{ex}");
            }

            //default
            return @"";
        }

        public string[] ParseMasterUrls(string playlist)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //maximum Disney+ Widevine quality is 720p
                    var maxQuality = @"1280x720";

                    //audio playlist URL
                    var audioUrl = ParseMasterAudioPlaylist(playlist);

                    //debugging only!
                    MessageBox.Show(audioUrl);
                }
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Parse master error:\n\n{ex}");
            }

            //default
            return null;
        }

        public string PlaylistMapUrl(string playlist)
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
                        if (maps.Count > 1)
                            return maps[1];
                    }
                    else
                        MessageBox.Show(@"Null playlist parse result!");
                }
                else
                    MessageBox.Show(@"Null or empty playlist supplied!");
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Playlist parse error:\n\n{ex}");
            }

            //default
            return @"";
        }

        public bool PlaylistValid(string playlist)
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

        public string PlaylistDownload(string playlistUrl, bool noGlobal = false)
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
                    {
                        //assign the global
                        if (!noGlobal)
                            GlobalPlaylist = playlist;

                        //return downloaded playlist
                        return playlist;
                    }
                }
                else
                    MessageBox.Show(@"Incorrect content/playlist URL!");
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Playlist download error:\n\n{ex}");
            }

            //default
            return @"";
        }

        public void PlaylistDownloadSegments(string playlist, string baseUri, string filePath = @"audio.bin")
        {
            try
            {
                //constants
                const string correctUrlComponent = @"-MAIN/";

                //validation
                if (!string.IsNullOrWhiteSpace(playlist))
                {
                    //parse playlist
                    var p = new PlaylistParser().Parse(playlist);

                    //validation
                    if (p != null)
                    {
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
                                    //validate it using the 'MAIN' audio component check
                                    if (uri.Contains(correctUrlComponent))
                                    {
                                        //fully-qualified segment URL
                                        var segmentUrl = $"{baseUri}{uri}";

                                        //try download of segment
                                        var segment = ResourceGrab.GrabBytes(segmentUrl);

                                        //validation
                                        if (segment != null)
                                        {
                                            //flush to file
                                            WriteSegment(filePath, segment);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Playlist download error:\n\n{ex}");
            }
        }

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

        //https://www.techiedelight.com/concatenate-byte-arrays-csharp/
        public static byte[] Combine(byte[] first, byte[] second)
        {
            byte[] bytes = new byte[first.Length + second.Length];
            Buffer.BlockCopy(first, 0, bytes, 0, first.Length);
            Buffer.BlockCopy(second, 0, bytes, first.Length, second.Length);
            return bytes;
        }

        public void DoDecrypt(string inputFile, string outputFile, string key)
        {
            try
            {
                const string fileName = @"mp4decrypt.exe";

                if (File.Exists(fileName))
                {
                    //declare bento4 executable startup
                    var p = new Process
                    {
                        StartInfo =
                        {
                            FileName = fileName,
                            CreateNoWindow = true,
                            Arguments = $"--key 1:{key} --show-progress {inputFile} {outputFile}"
                        }
                    };

                    //execute the decryptor
                    p.Start();
                }
                else
                    MessageBox.Show(@"Couldn't decrypt because mp4decrypt.exe was not found");
            }
            catch (Exception ex)
            {
                //report error
                MessageBox.Show($"Decryption error:\n\n{ex}");
            }
        }

        public string GetBaseUrl(string url) => new Uri(new Uri(url), ".").OriginalString;

        private void BtnAssembleAndDecrypt_Click(object sender, EventArgs e)
        {
            //validation
            if (string.IsNullOrWhiteSpace(txtDecryptionKey.Text) || string.IsNullOrWhiteSpace(txtContentURL.Text))
                MessageBox.Show(@"Please enter both the master playlist URL and the content decryption key");
            else
            {
                //playlist URL is declared via a TextBox
                var masterUrl = txtContentURL.Text;

                //download playlist
                var masterPlaylist = PlaylistDownload(masterUrl);

                //primary validation
                if (!string.IsNullOrWhiteSpace(masterPlaylist))
                    //validate it
                    if (PlaylistValid(masterPlaylist))
                    {
                        //master base URL
                        var masterBaseUrl = GetBaseUrl(masterUrl);

                        //try and get audio URL from master
                        var audioPath = ParseMasterAudioPlaylist(masterPlaylist);

                        //audio fully-qualified URL
                        var audioUrl = $"{masterBaseUrl}{audioPath}";

                        //audio base URL
                        var audioBaseUrl = GetBaseUrl(audioUrl);

                        //try and download audio playlist
                        var audioPlaylist = PlaylistDownload(audioUrl, true);

                        //validate it
                        if (!string.IsNullOrWhiteSpace(audioPlaylist))
                        {
                            //validate audio playlist
                            if (PlaylistValid(audioPlaylist))
                            {
                                //flush audio playlist to disk
                                File.WriteAllText(@"audio_playlist.m3u8", audioPlaylist);

                                //map URL
                                var audioMapUrl = $"{audioBaseUrl}{PlaylistMapUrl(audioPlaylist)}";

                                //download the map
                                var audioMap = ResourceGrab.GrabBytes(audioMapUrl);

                                //validate
                                if (audioMap != null)
                                {
                                    //flush to disk
                                    File.WriteAllBytes(@"audioMap.bin", audioMap);

                                    //inform the user
                                    MessageBox.Show(@"Map downloaded! Downloading full audio playlist with all segments");

                                    //download all segments if the bin doesn't already exist
                                    if (!File.Exists(@"audioDat.bin"))
                                        PlaylistDownloadSegments(audioPlaylist, audioBaseUrl, @"audioDat.bin");

                                    //inform the user
                                    //MessageBox.Show(@"Playlist downloaded! Assembling full audio and attempting decrypt");

                                    //read in segment data
                                    var audioSegments = File.ReadAllBytes(@"audioDat.bin");

                                    //combine map and segment data
                                    var audioCombined = Combine(audioMap, audioSegments);

                                    //flush it to disk
                                    File.WriteAllBytes(@"audioEncrypted.mp4", audioCombined);

                                    //attempt decrypt operation
                                    DoDecrypt(@"audioEncrypted.mp4", @"audioDecrypted.mp4", txtDecryptionKey.Text);

                                    //MessageBox.Show(@"Assembly and decryption process completed");

                                    //video time!
                                    MessageBox.Show(@"Audio playlist downloaded and decrypted; time for video. This may take a very long time.");

                                    //video playlist parse
                                    var videoPlaylistPath = ParseMasterVideoPlaylist(masterPlaylist);

                                    //validation
                                    if (!string.IsNullOrWhiteSpace(videoPlaylistPath))
                                    {
                                        //fully-qualified URL
                                        var videoPlaylistUrl = $"{masterBaseUrl}{videoPlaylistPath}";

                                        //try download of playlist
                                        var videoPlaylist = ResourceGrab.GrabString(videoPlaylistUrl);
                                        var videoBaseUrl = GetBaseUrl(videoPlaylistUrl);

                                        //validation
                                        if (PlaylistValid(videoPlaylist))
                                        {
                                            //get map URL
                                            var videoMapPath = PlaylistMapUrl(videoPlaylist);
                                            var videoMapUrl = $"{videoBaseUrl}{videoMapPath}";

                                            //try map download
                                            var videoMap = ResourceGrab.GrabBytes(videoMapUrl);

                                            //flush to file
                                            File.WriteAllBytes(@"videoMap.bin", videoMap);

                                            //download all video segments
                                            if (!File.Exists(@"videoDat.bin"))
                                                PlaylistDownloadSegments(videoPlaylist, videoBaseUrl, @"videoDat.bin");

                                            //read in segment data
                                            var videoSegments = File.ReadAllBytes(@"videoDat.bin");

                                            //combine map and segment data
                                            var videoCombined = Combine(videoMap, videoSegments);

                                            //flush to disk
                                            File.WriteAllBytes(@"videoEncrypted.mp4", videoCombined);

                                            //attempt decrypt operation
                                            DoDecrypt(@"videoEncrypted.mp4", @"videoDecrypted.mp4", txtDecryptionKey.Text);

                                            //report success
                                            MessageBox.Show(@"Done!");
                                        }
                                        else
                                            MessageBox.Show(@"Invalid Disney+ Video HLS Playlist!");
                                    }
                                    else
                                        MessageBox.Show(@"Video download failed! Null URL result received");
                                }
                                else
                                    MessageBox.Show(@"Invalid audio init.mp4! Null result received");
                            }
                            else
                                MessageBox.Show(@"Invalid audio playlist! Does not conform");
                        }
                        else
                            MessageBox.Show(@"Invalid audio playlist! Null result received");
                    }
                    else
                        MessageBox.Show(@"Invalid Disney+ HLS Playlist!");
            }
        }
    }
}
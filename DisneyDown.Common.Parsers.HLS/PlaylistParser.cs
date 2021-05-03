using DisneyDown.Common.Parsers.HLS.Playlist;
using Sprache;
using System.Collections.Generic;
using System.IO;

namespace DisneyDown.Common.Parsers.HLS
{
    public class PlaylistParser
    {
        public Playlist.Playlist Parse(string playlist)
        {
            //temporary IO provider
            using (var stringReader = new StringReader(playlist))
            {
                //new HLS items list
                var playListItems = new List<PlaylistItem>();

                //temporary line buffer
                string line;

                //parse each line in the playlist
                while ((line = stringReader.ReadLine()) != null)
                    playListItems.Add(PlaylistGrammar.PlaylistParser.Parse(line));

                //create playlist
                var playlistProvider = new Playlist.Playlist(playListItems);

                //return final result
                return playlistProvider;
            }
        }
    }
}
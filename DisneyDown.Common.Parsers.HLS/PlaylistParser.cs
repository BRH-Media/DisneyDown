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
            using (var stringReader = new StringReader(playlist))
            {
                var playListItems = new List<PlaylistItem>();
                string line;
                while ((line = stringReader.ReadLine()) != null)
                {
                    playListItems.Add(PlaylistGrammar.PlaylistParser.Parse(line));
                }
                return new Playlist.Playlist(playListItems);
            }
        }
    }
}
namespace DisneyDown.Common.Interpreters.HLSParser.Playlist
{
    public class PlaylistUriItem : PlaylistItem
    {
        public string Uri { get; }

        public PlaylistUriItem(string uri)
        {
            Uri = uri;
        }
    }
}
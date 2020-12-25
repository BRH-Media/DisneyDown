namespace DisneyDown.Common.Parsers.HLS.Playlist
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
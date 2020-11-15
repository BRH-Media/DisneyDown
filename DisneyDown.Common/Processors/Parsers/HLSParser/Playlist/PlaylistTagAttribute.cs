namespace DisneyDown.Common.Processors.Parsers.HLSParser.Playlist
{
    public class PlaylistTagAttribute
    {
        public PlaylistTagAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; }
        public string Value { get; }
    }
}
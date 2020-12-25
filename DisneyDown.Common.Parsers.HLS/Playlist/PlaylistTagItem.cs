using System.Collections.Generic;
using System.Linq;

namespace DisneyDown.Common.Parsers.HLS.Playlist
{
    public class PlaylistTagItem : PlaylistItem
    {
        public PlaylistTagItem(PlaylistTagId id, string value)
        {
            Id = id;
            RawValue = value;
        }

        public PlaylistTagId Id { get; }
        public List<PlaylistTagAttribute> Attributes { get; }

        public string Value
        {
            get
            {
                if (HasValue())
                {
                    return Attributes[0].Key;
                }
                return string.Empty;
            }
        }

        public string RawValue { get; set; }

        public PlaylistTagItem(PlaylistTagId id, List<PlaylistTagAttribute> attributes)
        {
            Id = id;
            Attributes = attributes;
        }

        public bool HasAttributes()
        {
            return (Attributes.Any() &&
               !string.IsNullOrWhiteSpace(Attributes[0].Key) &&
               !string.IsNullOrWhiteSpace(Attributes[0].Value));
        }

        public bool HasValue()
        {
            return Attributes.Any() &&
               !string.IsNullOrWhiteSpace(Attributes[0].Key) &&
               string.IsNullOrWhiteSpace(Attributes[0].Value);
        }
    }
}
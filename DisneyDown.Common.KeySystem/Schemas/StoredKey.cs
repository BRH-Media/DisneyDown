using System;

namespace DisneyDown.Common.KeySystem.Schemas
{
    public class StoredKey
    {
        public string WvKeyId { get; set; }
        public string WvKey { get; set; }
        public string PlaybackDomain { get; set; }
        public DateTimeOffset Timestamp { get; set; }
    }
}
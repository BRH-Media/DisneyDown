using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas.MP4Dump
{
    public partial class PurpleChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public long? Enabled { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Duration { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public FluffyChild[] Children { get; set; }

        [JsonProperty("track id", NullValueHandling = NullValueHandling.Ignore)]
        public long? TrackId { get; set; }

        [JsonProperty("default sample description index", NullValueHandling = NullValueHandling.Ignore)]
        public long? DefaultSampleDescriptionIndex { get; set; }

        [JsonProperty("default sample duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? DefaultSampleDuration { get; set; }

        [JsonProperty("default sample size", NullValueHandling = NullValueHandling.Ignore)]
        public long? DefaultSampleSize { get; set; }

        [JsonProperty("default sample flags", NullValueHandling = NullValueHandling.Ignore)]
        public long? DefaultSampleFlags { get; set; }
    }
}
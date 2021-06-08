using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas
{
    public partial class Mp4DumpSchemaChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("timescale", NullValueHandling = NullValueHandling.Ignore)]
        public long? Timescale { get; set; }

        [JsonProperty("duration", NullValueHandling = NullValueHandling.Ignore)]
        public long? Duration { get; set; }

        [JsonProperty("duration(ms)", NullValueHandling = NullValueHandling.Ignore)]
        public long? DurationMs { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleChild[] Children { get; set; }
    }
}
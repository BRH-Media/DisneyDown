using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas
{
    public partial class StickyChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public IndigoChild[] Children { get; set; }

        [JsonProperty("entry_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? EntryCount { get; set; }

        [JsonProperty("sample_size", NullValueHandling = NullValueHandling.Ignore)]
        public long? SampleSize { get; set; }

        [JsonProperty("sample_count", NullValueHandling = NullValueHandling.Ignore)]
        public long? SampleCount { get; set; }
    }
}
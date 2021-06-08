using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas
{
    public partial class HilariousChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("original_format", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalFormat { get; set; }

        [JsonProperty("scheme_type", NullValueHandling = NullValueHandling.Ignore)]
        public string SchemeType { get; set; }

        [JsonProperty("scheme_version", NullValueHandling = NullValueHandling.Ignore)]
        public long? SchemeVersion { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public AmbitiousChild[] Children { get; set; }
    }
}
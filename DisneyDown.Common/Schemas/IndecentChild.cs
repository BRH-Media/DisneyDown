using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas
{
    public partial class IndecentChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("Configuration Version", NullValueHandling = NullValueHandling.Ignore)]
        public long? ConfigurationVersion { get; set; }

        [JsonProperty("Profile", NullValueHandling = NullValueHandling.Ignore)]
        public string Profile { get; set; }

        [JsonProperty("Profile Compatibility", NullValueHandling = NullValueHandling.Ignore)]
        public long? ProfileCompatibility { get; set; }

        [JsonProperty("Level", NullValueHandling = NullValueHandling.Ignore)]
        public long? Level { get; set; }

        [JsonProperty("NALU Length Size", NullValueHandling = NullValueHandling.Ignore)]
        public long? NaluLengthSize { get; set; }

        [JsonProperty("Sequence Parameter", NullValueHandling = NullValueHandling.Ignore)]
        public string SequenceParameter { get; set; }

        [JsonProperty("Picture Parameter", NullValueHandling = NullValueHandling.Ignore)]
        public string PictureParameter { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public HilariousChild[] Children { get; set; }
    }
}
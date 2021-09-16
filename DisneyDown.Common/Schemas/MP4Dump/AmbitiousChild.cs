using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas.MP4Dump
{
    public partial class AmbitiousChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("default_isProtected")]
        public long DefaultIsProtected { get; set; }

        [JsonProperty("default_Per_Sample_IV_Size")]
        public long DefaultPerSampleIvSize { get; set; }

        [JsonProperty("default_KID")]
        public string DefaultKid { get; set; }
    }
}
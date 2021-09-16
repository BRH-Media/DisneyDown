using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas.MP4Dump
{
    public partial class TentacledChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("graphics_mode", NullValueHandling = NullValueHandling.Ignore)]
        public long? GraphicsMode { get; set; }

        [JsonProperty("op_color", NullValueHandling = NullValueHandling.Ignore)]
        public string OpColor { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public StickyChild[] Children { get; set; }
    }
}
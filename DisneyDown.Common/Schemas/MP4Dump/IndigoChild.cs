using Newtonsoft.Json;

namespace DisneyDown.Common.Schemas.MP4Dump
{
    public partial class IndigoChild
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("header_size")]
        public long HeaderSize { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("location", NullValueHandling = NullValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty("data_reference_index", NullValueHandling = NullValueHandling.Ignore)]
        public long? DataReferenceIndex { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("compressor", NullValueHandling = NullValueHandling.Ignore)]
        public string Compressor { get; set; }

        [JsonProperty("children", NullValueHandling = NullValueHandling.Ignore)]
        public IndecentChild[] Children { get; set; }
    }
}
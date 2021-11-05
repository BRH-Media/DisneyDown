using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Qos
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("isFilterable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool IsFilterable { get; set; }

        [JsonProperty("imageAspectRatio")]
        public string ImageAspectRatio { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("cdnVendor")]
        public string CdnVendor { get; set; }

        [JsonProperty("pool")]
        public string Pool { get; set; }

        [JsonProperty("encodingFeatures")]
        public string EncodingFeatures { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("cdnWithOrigin")]
        public string CdnWithOrigin { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }

        [JsonProperty("cdnName")]
        public string CdnName { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }
    }
}
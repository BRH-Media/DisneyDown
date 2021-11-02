using System;
using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class FluffyQos
    {
        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("isFilterable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool IsFilterable { get; set; }

        [JsonProperty("imageAspectRatio")]
        public string ImageAspectRatio { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("pool")]
        public string Pool { get; set; }

        [JsonProperty("mediaType")]
        public string MediaType { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("platform")]
        public string Platform { get; set; }
    }
}
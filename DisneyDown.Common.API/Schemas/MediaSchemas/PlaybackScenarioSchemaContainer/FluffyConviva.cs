using System;
using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class FluffyConviva
    {
        [JsonProperty("pbs")]
        public string Pbs { get; set; }

        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("isFilterable")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool IsFilterable { get; set; }

        [JsonProperty("imageAspectRatio")]
        public string ImageAspectRatio { get; set; }

        [JsonProperty("capabilities")]
        public string Capabilities { get; set; }

        [JsonProperty("experiments")]
        public string Experiments { get; set; }

        [JsonProperty("pool")]
        public string Pool { get; set; }

        [JsonProperty("encryptionType")]
        public string EncryptionType { get; set; }

        [JsonProperty("med")]
        public string Med { get; set; }

        [JsonProperty("userid")]
        public Guid Userid { get; set; }

        [JsonProperty("deviceId")]
        public Guid DeviceId { get; set; }

        [JsonProperty("baseDevice")]
        public string BaseDevice { get; set; }

        [JsonProperty("filter")]
        public string Filter { get; set; }

        [JsonProperty("prt")]
        public string Prt { get; set; }

        [JsonProperty("videoSegmentTypes")]
        public string VideoSegmentTypes { get; set; }

        [JsonProperty("videoRanges")]
        public string VideoRanges { get; set; }

        [JsonProperty("videoCodecs")]
        public string VideoCodecs { get; set; }

        [JsonProperty("plt")]
        public string Plt { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }

        [JsonProperty("conid")]
        public Guid Conid { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }
    }
}
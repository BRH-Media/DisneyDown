using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Attributes
    {
        [JsonProperty("drms")]
        public string[] Drms { get; set; }

        [JsonProperty("encryptionType")]
        public string EncryptionType { get; set; }

        [JsonProperty("audioSegmentTypes")]
        public object[] AudioSegmentTypes { get; set; }

        [JsonProperty("videoSegmentTypes")]
        public object[] VideoSegmentTypes { get; set; }

        [JsonProperty("videoRanges")]
        public string[] VideoRanges { get; set; }

        [JsonProperty("security")]
        public Security Security { get; set; }

        [JsonProperty("experienceContext")]
        public string ExperienceContext { get; set; }
    }
}
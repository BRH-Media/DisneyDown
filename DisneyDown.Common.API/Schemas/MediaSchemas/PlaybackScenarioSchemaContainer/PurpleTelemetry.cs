using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PurpleTelemetry
    {
        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("cdnPolicyId")]
        public string CdnPolicyId { get; set; }
    }
}
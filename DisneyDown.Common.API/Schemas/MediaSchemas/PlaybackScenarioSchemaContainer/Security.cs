using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Security
    {
        [JsonProperty("drmEndpointKey")]
        public string DrmEndpointKey { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Security
    {
        [JsonProperty("drmEndpointKey")]
        public string DrmEndpointKey { get; set; }
    }
}
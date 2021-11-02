using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Headers
    {
        [JsonProperty("x-playback-scenario-name")]
        public string XPlaybackScenarioName { get; set; }

        [JsonProperty("accept")]
        public string Accept { get; set; }

        [JsonProperty("x-playback-request-id")]
        public string XPlaybackRequestId { get; set; }
    }
}
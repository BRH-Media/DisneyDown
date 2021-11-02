using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PlaybackScenarioSchemaTracking
    {
        [JsonProperty("conviva")]
        public FluffyConviva Conviva { get; set; }

        [JsonProperty("telemetry")]
        public FluffyTelemetry Telemetry { get; set; }

        [JsonProperty("adEngine")]
        public FluffyAdEngine AdEngine { get; set; }

        [JsonProperty("qos")]
        public FluffyQos Qos { get; set; }
    }
}
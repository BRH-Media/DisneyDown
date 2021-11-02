using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class CompleteTracking
    {
        [JsonProperty("conviva")]
        public PurpleConviva Conviva { get; set; }

        [JsonProperty("telemetry")]
        public PurpleTelemetry Telemetry { get; set; }

        [JsonProperty("adEngine")]
        public PurpleAdEngine AdEngine { get; set; }

        [JsonProperty("qos")]
        public PurpleQos Qos { get; set; }
    }
}
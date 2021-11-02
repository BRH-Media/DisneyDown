using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Tracking
    {
        [JsonProperty("conviva")]
        public Conviva Conviva { get; set; }

        [JsonProperty("telemetry")]
        public Telemetry Telemetry { get; set; }

        [JsonProperty("adEngine")]
        public AdEngine AdEngine { get; set; }

        [JsonProperty("qos")]
        public Qos Qos { get; set; }
    }
}
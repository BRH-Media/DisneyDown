using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Telemetry
    {
        [JsonProperty("client")]
        public TelemetryClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public TelemetryExtras Extras { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PingPolicy
    {
        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("pingInterval")]
        public long PingInterval { get; set; }

        [JsonProperty("pingMaxAttempts")]
        public long PingMaxAttempts { get; set; }

        [JsonProperty("pongTimeout")]
        public long PongTimeout { get; set; }
    }
}
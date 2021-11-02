using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PlaybackScenarios
    {
        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("limited")]
        public string Limited { get; set; }

        [JsonProperty("metered")]
        public string Metered { get; set; }

        [JsonProperty("unlimited")]
        public string Unlimited { get; set; }
    }
}
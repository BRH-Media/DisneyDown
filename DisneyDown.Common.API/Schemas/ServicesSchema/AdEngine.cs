using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class AdEngine
    {
        [JsonProperty("client")]
        public AdEngineClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public AdEngineExtras Extras { get; set; }
    }
}
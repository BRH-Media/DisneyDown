using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Token
    {
        [JsonProperty("client")]
        public TokenClient Client { get; set; }

        [JsonProperty("extras")]
        public TokenExtras Extras { get; set; }
    }
}
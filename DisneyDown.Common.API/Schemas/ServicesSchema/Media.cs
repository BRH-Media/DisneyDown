using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Media
    {
        [JsonProperty("client")]
        public MediaClient Client { get; set; }

        [JsonProperty("extras")]
        public MediaExtras Extras { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Commerce
    {
        [JsonProperty("client")]
        public CommerceClient Client { get; set; }

        [JsonProperty("extras")]
        public CommerceExtras Extras { get; set; }
    }
}
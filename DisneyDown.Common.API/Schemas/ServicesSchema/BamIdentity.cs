using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class BamIdentity
    {
        [JsonProperty("client")]
        public AccountClient Client { get; set; }

        [JsonProperty("extras")]
        public BamIdentityExtras Extras { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Account
    {
        [JsonProperty("client")]
        public AccountClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Entitlement
    {
        [JsonProperty("client")]
        public EntitlementClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Paywall
    {
        [JsonProperty("client")]
        public PaywallClient Client { get; set; }
    }
}
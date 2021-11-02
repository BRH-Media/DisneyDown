using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Subscription
    {
        [JsonProperty("client")]
        public SubscriptionClient Client { get; set; }
    }
}
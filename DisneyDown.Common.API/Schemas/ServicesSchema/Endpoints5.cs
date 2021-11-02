using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints5
    {
        [JsonProperty("getAccountSubscription")]
        public Endpoint GetAccountSubscription { get; set; }

        [JsonProperty("getSubscriberInfo")]
        public Endpoint GetSubscriberInfo { get; set; }

        [JsonProperty("getSubscriptions")]
        public Endpoint GetSubscriptions { get; set; }

        [JsonProperty("linkSubscriptionsFromDevice")]
        public Endpoint LinkSubscriptionsFromDevice { get; set; }
    }
}
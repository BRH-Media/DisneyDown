using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CommerceExtras
    {
        [JsonProperty("checkOrderStatusDelay")]
        public long CheckOrderStatusDelay { get; set; }

        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PurchaseExtras
    {
        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }
    }
}
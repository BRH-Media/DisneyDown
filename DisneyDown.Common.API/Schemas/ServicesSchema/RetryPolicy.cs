using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class RetryPolicy
    {
        [JsonProperty("retryBasePeriod")]
        public long RetryBasePeriod { get; set; }

        [JsonProperty("retryMaxAttempts")]
        public long RetryMaxAttempts { get; set; }

        [JsonProperty("retryMaxPeriod")]
        public long RetryMaxPeriod { get; set; }

        [JsonProperty("retryMultiplier")]
        public double RetryMultiplier { get; set; }
    }
}
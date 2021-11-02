using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CdnFallback
    {
        [JsonProperty("fallbackLimit")]
        public long FallbackLimit { get; set; }

        [JsonProperty("isEnabled")]
        public bool IsEnabled { get; set; }
    }
}
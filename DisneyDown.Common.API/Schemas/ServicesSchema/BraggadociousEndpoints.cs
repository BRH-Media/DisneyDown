using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class BraggadociousEndpoints
    {
        [JsonProperty("exchangeDeviceGrantForAccessToken")]
        public ExchangeDeviceGrantForAccessToken ExchangeDeviceGrantForAccessToken { get; set; }

        [JsonProperty("globalization")]
        public ExchangeDeviceGrantForAccessToken Globalization { get; set; }

        [JsonProperty("query")]
        public ExchangeDeviceGrantForAccessToken Query { get; set; }

        [JsonProperty("refreshToken")]
        public ExchangeDeviceGrantForAccessToken RefreshToken { get; set; }

        [JsonProperty("registerDevice")]
        public ExchangeDeviceGrantForAccessToken RegisterDevice { get; set; }
    }
}
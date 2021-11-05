using Newtonsoft.Json;
using RestSharp;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ExchangeDeviceGrantForAccessToken
    {
        [JsonProperty("headers")]
        public EndpointHeaders Headers { get; set; }

        [JsonProperty("href")]
        public Uri Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("optionalHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public ExchangeDeviceGrantForAccessTokenOptionalHeaders OptionalHeaders { get; set; }
    }
}
using Newtonsoft.Json;
using RestSharp;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoint
    {
        [JsonProperty("headers")]
        public EndpointHeaders Headers { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("method")]
        public Method Method { get; set; }

        [JsonProperty("templated")]
        public bool Templated { get; set; }

        [JsonProperty("timeout")]
        public long Timeout { get; set; }

        [JsonProperty("ttl")]
        public long Ttl { get; set; }

        [JsonProperty("optionalHeaders", NullValueHandling = NullValueHandling.Ignore)]
        public EndpointOptionalHeaders OptionalHeaders { get; set; }
    }
}
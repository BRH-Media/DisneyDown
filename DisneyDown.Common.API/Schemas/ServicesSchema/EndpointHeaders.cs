using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class EndpointHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }

        [JsonProperty("X-Bamtech-Request-Id", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamtechRequestId { get; set; }

        [JsonProperty("SOAPAction", NullValueHandling = NullValueHandling.Ignore)]
        public Uri SoapAction { get; set; }

        [JsonProperty("X-DSS-Feature-Filtering", NullValueHandling = NullValueHandling.Ignore)]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool? XDssFeatureFiltering { get; set; }

        [JsonProperty("X-BAMSDK-Platform-Id", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamsdkPlatformId { get; set; }
    }
}
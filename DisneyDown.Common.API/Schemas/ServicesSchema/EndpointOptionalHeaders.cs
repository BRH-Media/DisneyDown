using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class EndpointOptionalHeaders
    {
        [JsonProperty("X-BAMTech-Purchase-Platform", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamTechPurchasePlatform { get; set; }

        [JsonProperty("X-BAMTech-Sales-Platform", NullValueHandling = NullValueHandling.Ignore)]
        public string XBamTechSalesPlatform { get; set; }

        [JsonProperty("X-Content-Transaction-ID", NullValueHandling = NullValueHandling.Ignore)]
        public string XContentTransactionId { get; set; }

        [JsonProperty("X-DELOREAN", NullValueHandling = NullValueHandling.Ignore)]
        public string XDelorean { get; set; }

        [JsonProperty("X-GEO-OVERRIDE", NullValueHandling = NullValueHandling.Ignore)]
        public string XGeoOverride { get; set; }
    }
}
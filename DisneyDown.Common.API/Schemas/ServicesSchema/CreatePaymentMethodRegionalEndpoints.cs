using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CreatePaymentMethodRegionalEndpoints
    {
        [JsonProperty("eu-central-1")]
        public string EuCentral1 { get; set; }

        [JsonProperty("eu-west-1")]
        public string EuWest1 { get; set; }

        [JsonProperty("us-east-1")]
        public string UsEast1 { get; set; }

        [JsonProperty("us-east-2", NullValueHandling = NullValueHandling.Ignore)]
        public string UsEast2 { get; set; }

        [JsonProperty("us-west-2")]
        public string UsWest2 { get; set; }
    }
}
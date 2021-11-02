using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class GetDefaultPaymentMethodHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
    }
}
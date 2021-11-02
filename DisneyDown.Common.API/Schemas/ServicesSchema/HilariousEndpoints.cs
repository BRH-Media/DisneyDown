using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class HilariousEndpoints
    {
        [JsonProperty("getEligibilityStatus")]
        public Endpoint GetEligibilityStatus { get; set; }
    }
}
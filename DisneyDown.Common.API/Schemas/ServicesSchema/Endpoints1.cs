using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints1
    {
        [JsonProperty("paywall")]
        public Endpoint Paywall { get; set; }
    }
}
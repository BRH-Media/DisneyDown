using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class AdEngineClient
    {
        [JsonProperty("baseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("endpoints")]
        public PurpleEndpoints Endpoints { get; set; }
    }
}
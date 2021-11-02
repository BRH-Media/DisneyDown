using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints4
    {
        [JsonProperty("getInfo")]
        public Endpoint GetInfo { get; set; }

        [JsonProperty("getLocation")]
        public Endpoint GetLocation { get; set; }
    }
}
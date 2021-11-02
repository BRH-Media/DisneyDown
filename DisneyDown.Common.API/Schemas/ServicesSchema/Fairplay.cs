using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Fairplay
    {
        [JsonProperty("event")]
        public string Event { get; set; }

        [JsonProperty("linear")]
        public string Linear { get; set; }

        [JsonProperty("vod")]
        public string Vod { get; set; }
    }
}
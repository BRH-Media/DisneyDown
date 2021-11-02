using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class FastTrack
    {
        [JsonProperty("urns")]
        public string[] Urns { get; set; }
    }
}
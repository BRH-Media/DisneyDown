using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints6
    {
        [JsonProperty("dustEvent")]
        public SetTokenPost DustEvent { get; set; }

        [JsonProperty("postEvent")]
        public SetTokenPost PostEvent { get; set; }

        [JsonProperty("validateDustEvent")]
        public SetTokenPost ValidateDustEvent { get; set; }
    }
}
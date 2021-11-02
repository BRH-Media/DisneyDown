using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SetTokenPostHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
    }
}
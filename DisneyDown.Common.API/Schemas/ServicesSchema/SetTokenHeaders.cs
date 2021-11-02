using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SetTokenHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SubjectTokenTypes
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("temporary")]
        public string Temporary { get; set; }
    }
}
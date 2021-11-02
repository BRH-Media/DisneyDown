using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ResourcesPersistedHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Content-Type")]
        public string ContentType { get; set; }
    }
}
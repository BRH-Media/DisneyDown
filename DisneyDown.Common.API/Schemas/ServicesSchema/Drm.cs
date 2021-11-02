using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Drm
    {
        [JsonProperty("client")]
        public DrmClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Content
    {
        [JsonProperty("client")]
        public ContentClient Client { get; set; }

        [JsonProperty("extras")]
        public ContentExtras Extras { get; set; }
    }
}
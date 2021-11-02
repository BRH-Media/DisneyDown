using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class DmcVideo
    {
        [JsonProperty("video")]
        public Video Video { get; set; }
    }
}
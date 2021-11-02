using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class VideoArtMediaMetadata
    {
        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }
}
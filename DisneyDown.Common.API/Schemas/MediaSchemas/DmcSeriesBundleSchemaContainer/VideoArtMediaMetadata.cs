using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class VideoArtMediaMetadata
    {
        [JsonProperty("urls")]
        public Url[] Urls { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class VideoText
    {
        [JsonProperty("title")]
        public FluffyTitle Title { get; set; }

        [JsonProperty("description")]
        public FluffyDescription Description { get; set; }
    }
}
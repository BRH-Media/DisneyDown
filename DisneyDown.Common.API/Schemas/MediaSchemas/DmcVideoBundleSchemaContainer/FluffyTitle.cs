using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class FluffyTitle
    {
        [JsonProperty("full")]
        public FluffyBrief Full { get; set; }

        [JsonProperty("slug")]
        public FluffyBrief Slug { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class FluffyTitle
    {
        [JsonProperty("slug")]
        public FluffyBrief Slug { get; set; }

        [JsonProperty("full")]
        public FluffyBrief Full { get; set; }
    }
}
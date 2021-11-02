using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class PurpleTitle
    {
        [JsonProperty("slug")]
        public PurpleBrief Slug { get; set; }

        [JsonProperty("full")]
        public PurpleBrief Full { get; set; }
    }
}
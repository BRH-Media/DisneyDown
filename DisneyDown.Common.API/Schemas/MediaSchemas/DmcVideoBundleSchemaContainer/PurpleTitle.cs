using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class PurpleTitle
    {
        [JsonProperty("full")]
        public PurpleBrief Full { get; set; }

        [JsonProperty("slug")]
        public PurpleBrief Slug { get; set; }
    }
}
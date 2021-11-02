using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class ItemText
    {
        [JsonProperty("title")]
        public PurpleTitle Title { get; set; }

        [JsonProperty("description")]
        public PurpleDescription Description { get; set; }
    }
}
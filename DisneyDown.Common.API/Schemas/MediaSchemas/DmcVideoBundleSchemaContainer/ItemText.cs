using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class ItemText
    {
        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("description")]
        public FluffyDescription Description { get; set; }
    }
}
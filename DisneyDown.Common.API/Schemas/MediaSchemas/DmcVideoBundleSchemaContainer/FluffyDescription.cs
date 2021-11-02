using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class FluffyDescription
    {
        [JsonProperty("brief")]
        public FluffyBrief Brief { get; set; }

        [JsonProperty("medium")]
        public FluffyBrief Medium { get; set; }

        [JsonProperty("full")]
        public FluffyBrief Full { get; set; }
    }
}
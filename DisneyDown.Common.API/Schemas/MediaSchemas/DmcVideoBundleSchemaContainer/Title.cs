using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Title
    {
        [JsonProperty("full")]
        public Brief Full { get; set; }

        [JsonProperty("slug")]
        public Brief Slug { get; set; }
    }
}
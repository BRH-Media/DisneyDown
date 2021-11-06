using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Title
    {
        [JsonProperty("slug")]
        public Brief Slug { get; set; }

        [JsonProperty("sort")]
        public Brief Sort { get; set; }

        [JsonProperty("full")]
        public Brief Full { get; set; }
    }
}
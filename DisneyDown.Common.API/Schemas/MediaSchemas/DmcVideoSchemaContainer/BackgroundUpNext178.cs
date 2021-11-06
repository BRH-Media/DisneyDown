using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class BackgroundUpNext178
    {
        [JsonProperty("program")]
        public Series Program { get; set; }

        [JsonProperty("series")]
        public Series Series { get; set; }
    }
}
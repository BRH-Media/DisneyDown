using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Brief
    {
        [JsonProperty("program")]
        public Program Program { get; set; }

        [JsonProperty("series")]
        public Program Series { get; set; }

        [JsonProperty("season")]
        public Program Season { get; set; }
    }
}
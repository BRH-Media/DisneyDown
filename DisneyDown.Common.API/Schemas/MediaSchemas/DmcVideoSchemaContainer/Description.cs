using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Description
    {
        [JsonProperty("full")]
        public Brief Full { get; set; }

        [JsonProperty("brief")]
        public Brief Brief { get; set; }

        [JsonProperty("medium")]
        public Brief Medium { get; set; }
    }
}
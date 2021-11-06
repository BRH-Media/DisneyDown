using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class The178_Value
    {
        [JsonProperty("series")]
        public Series Series { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Data
    {
        [JsonProperty("DmcVideo")]
        public DmcVideo DmcVideo { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Data
    {
        [JsonProperty("DmcVideo")]
        public DmcVideo DmcVideo { get; set; }
    }
}
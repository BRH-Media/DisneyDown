using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Series
    {
        [JsonProperty("default")]
        public PurpleDefault Default { get; set; }
    }
}
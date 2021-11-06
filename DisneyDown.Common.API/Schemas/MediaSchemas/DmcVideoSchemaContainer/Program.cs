using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Program
    {
        [JsonProperty("default")]
        public SeasonDefault Default { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class SeasonDefault
    {
        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("sourceEntity")]
        public string SourceEntity { get; set; }
    }
}
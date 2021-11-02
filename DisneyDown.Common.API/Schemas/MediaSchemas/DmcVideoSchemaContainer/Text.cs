using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Text
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("sourceEntity")]
        public string SourceEntity { get; set; }

        [JsonProperty("targetEntity")]
        public string TargetEntity { get; set; }
    }
}
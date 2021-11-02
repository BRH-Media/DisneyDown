using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Meta
    {
        [JsonProperty("hits")]
        public long Hits { get; set; }

        [JsonProperty("offset")]
        public long Offset { get; set; }

        [JsonProperty("page_size")]
        public long PageSize { get; set; }
    }
}
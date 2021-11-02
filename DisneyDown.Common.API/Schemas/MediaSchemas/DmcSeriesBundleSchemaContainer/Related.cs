using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Related
    {
        [JsonProperty("experimentToken")]
        public string ExperimentToken { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
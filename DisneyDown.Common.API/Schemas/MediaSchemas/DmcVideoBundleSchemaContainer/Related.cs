using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Related
    {
        [JsonProperty("experimentToken")]
        public string ExperimentToken { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}
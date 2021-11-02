using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PurpleQos
    {
        [JsonProperty("cdnWithOrigin")]
        public string CdnWithOrigin { get; set; }

        [JsonProperty("cdnName")]
        public string CdnName { get; set; }

        [JsonProperty("cdnVendor")]
        public string CdnVendor { get; set; }
    }
}
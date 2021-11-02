using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PurpleConviva
    {
        [JsonProperty("cdnName")]
        public string CdnName { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("cdnVendor")]
        public string CdnVendor { get; set; }
    }
}
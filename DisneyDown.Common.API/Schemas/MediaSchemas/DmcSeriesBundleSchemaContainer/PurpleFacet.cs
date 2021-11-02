using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class PurpleFacet
    {
        [JsonProperty("activeAspectRatio")]
        public double ActiveAspectRatio { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Facet
    {
        [JsonProperty("activeAspectRatio")]
        public double ActiveAspectRatio { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }
    }
}
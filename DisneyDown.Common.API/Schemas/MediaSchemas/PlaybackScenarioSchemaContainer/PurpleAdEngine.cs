using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PurpleAdEngine
    {
        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("corigin")]
        public string Corigin { get; set; }
    }
}
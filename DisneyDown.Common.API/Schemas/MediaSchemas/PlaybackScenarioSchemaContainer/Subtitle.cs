using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Subtitle
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("forced")]
        public bool Forced { get; set; }
    }
}
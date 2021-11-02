using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Renditions
    {
        [JsonProperty("audio")]
        public Audio[] Audio { get; set; }

        [JsonProperty("subtitles")]
        public Subtitle[] Subtitles { get; set; }
    }
}
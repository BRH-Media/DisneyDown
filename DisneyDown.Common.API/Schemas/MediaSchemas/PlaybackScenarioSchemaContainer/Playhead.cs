using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Playhead
    {
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
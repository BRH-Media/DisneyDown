using Newtonsoft.Json;

// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class PlaybackScenarioSchema
    {
        [JsonProperty("stream")]
        public Stream Stream { get; set; }

        [JsonProperty("tracking")]
        public PlaybackScenarioSchemaTracking Tracking { get; set; }

        [JsonProperty("playhead")]
        public Playhead Playhead { get; set; }

        [JsonProperty("thumbnails")]
        public Thumbnails Thumbnails { get; set; }

        public static PlaybackScenarioSchema FromJson(string json) => JsonConvert.DeserializeObject<PlaybackScenarioSchema>(json, ApiJsonConverter.Settings);
    }
}
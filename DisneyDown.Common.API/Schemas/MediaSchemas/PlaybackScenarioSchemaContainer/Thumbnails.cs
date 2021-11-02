using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Thumbnails
    {
        [JsonProperty("bif")]
        public Bif Bif { get; set; }

        [JsonProperty("spritesheet")]
        public Bif Spritesheet { get; set; }

        [JsonProperty("bif-main")]
        public Bif BifMain { get; set; }
    }
}
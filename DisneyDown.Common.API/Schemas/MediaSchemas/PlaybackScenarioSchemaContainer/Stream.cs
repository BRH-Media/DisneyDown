using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Stream
    {
        [JsonProperty("renditions")]
        public Renditions Renditions { get; set; }

        [JsonProperty("attributes")]
        public Attributes Attributes { get; set; }

        [JsonProperty("variants")]
        public Variant[] Variants { get; set; }

        [JsonProperty("complete")]
        public Complete[] Complete { get; set; }
    }
}
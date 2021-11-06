using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class AudioTrack
    {
        [JsonProperty("features", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Features { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("renditionName")]
        public string RenditionName { get; set; }

        [JsonProperty("trackType")]
        public string TrackType { get; set; }
    }
}
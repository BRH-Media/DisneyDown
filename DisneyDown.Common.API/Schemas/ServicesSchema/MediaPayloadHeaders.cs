using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class MediaPayloadHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }

        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("X-DSS-Feature-Filtering")]
        [JsonConverter(typeof(ParseStringConverter))]
        public bool XDssFeatureFiltering { get; set; }
    }
}
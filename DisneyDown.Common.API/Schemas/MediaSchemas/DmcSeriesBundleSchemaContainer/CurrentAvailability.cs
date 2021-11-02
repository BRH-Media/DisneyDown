using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class CurrentAvailability
    {
        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("kidsMode")]
        public bool KidsMode { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class LicenseEndpoints
    {
        [JsonProperty("FAIRPLAY")]
        public Fairplay Fairplay { get; set; }

        [JsonProperty("NAGRA")]
        public Fairplay Nagra { get; set; }

        [JsonProperty("PLAYREADY")]
        public Fairplay Playready { get; set; }

        [JsonProperty("WIDEVINE")]
        public Fairplay Widevine { get; set; }
    }
}
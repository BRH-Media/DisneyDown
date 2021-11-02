using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class MediaExtras
    {
        [JsonProperty("cdnFallback")]
        public CdnFallback CdnFallback { get; set; }

        [JsonProperty("cookieEnabled")]
        public bool CookieEnabled { get; set; }

        [JsonProperty("isUhdAllowed")]
        public bool IsUhdAllowed { get; set; }

        [JsonProperty("playbackScenarioDefault")]
        public string PlaybackScenarioDefault { get; set; }

        [JsonProperty("playbackScenarios")]
        public PlaybackScenarios PlaybackScenarios { get; set; }

        [JsonProperty("playbackSession")]
        public PlaybackSession PlaybackSession { get; set; }

        [JsonProperty("restrictedPlaybackScenario")]
        public string RestrictedPlaybackScenario { get; set; }
    }
}
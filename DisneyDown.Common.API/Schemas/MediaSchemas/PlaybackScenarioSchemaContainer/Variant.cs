using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Variant
    {
        [JsonProperty("bandwidth")]
        public long Bandwidth { get; set; }

        [JsonProperty("resolution")]
        public string Resolution { get; set; }

        [JsonProperty("videoBytes")]
        public long VideoBytes { get; set; }

        [JsonProperty("maxAudioRenditionBytes")]
        public long MaxAudioRenditionBytes { get; set; }

        [JsonProperty("maxSubtitleRenditionBytes")]
        public long MaxSubtitleRenditionBytes { get; set; }

        [JsonProperty("audioChannels")]
        public long AudioChannels { get; set; }

        [JsonProperty("videoRange")]
        public string VideoRange { get; set; }

        [JsonProperty("videoCodec")]
        public string VideoCodec { get; set; }

        [JsonProperty("audioType")]
        public string AudioType { get; set; }

        [JsonProperty("audioCodec")]
        public string AudioCodec { get; set; }

        [JsonProperty("averageBandwidth")]
        public long AverageBandwidth { get; set; }

        [JsonProperty("frameRate")]
        public double FrameRate { get; set; }
    }
}
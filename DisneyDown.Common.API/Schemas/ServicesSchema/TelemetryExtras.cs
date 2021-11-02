using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TelemetryExtras
    {
        [JsonProperty("batchLimit")]
        public long BatchLimit { get; set; }

        [JsonProperty("bufferConfigurationDefault")]
        public BufferConfigurationDefault BufferConfigurationDefault { get; set; }

        [JsonProperty("eventBufferConfiguration")]
        public BufferConfigurationDefault EventBufferConfiguration { get; set; }

        [JsonProperty("fastTrack")]
        public FastTrack FastTrack { get; set; }

        [JsonProperty("glimpseBufferConfiguration")]
        public BufferConfigurationDefault GlimpseBufferConfiguration { get; set; }

        [JsonProperty("permitAppDustEvents")]
        public bool PermitAppDustEvents { get; set; }

        [JsonProperty("prohibited")]
        public FastTrack Prohibited { get; set; }

        [JsonProperty("qosBufferConfiguration")]
        public BufferConfigurationDefault QosBufferConfiguration { get; set; }

        [JsonProperty("replyAfterFallback")]
        public long ReplyAfterFallback { get; set; }

        [JsonProperty("streamSampleBufferConfiguration")]
        public BufferConfigurationDefault StreamSampleBufferConfiguration { get; set; }
    }
}
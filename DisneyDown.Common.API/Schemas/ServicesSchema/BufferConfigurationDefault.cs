using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class BufferConfigurationDefault
    {
        [JsonProperty("batchLimit")]
        public long BatchLimit { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("minimumBatchSize")]
        public long MinimumBatchSize { get; set; }

        [JsonProperty("replyAfterFallback")]
        public long ReplyAfterFallback { get; set; }
    }
}
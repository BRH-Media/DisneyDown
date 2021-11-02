using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class SocketExtras
    {
        [JsonProperty("messageDeduplicationStoreSize")]
        public long MessageDeduplicationStoreSize { get; set; }

        [JsonProperty("pingPolicy")]
        public PingPolicy PingPolicy { get; set; }

        [JsonProperty("retryPolicy")]
        public RetryPolicy RetryPolicy { get; set; }

        [JsonProperty("unacknowledgedEventBuffer")]
        public UnacknowledgedEventBuffer UnacknowledgedEventBuffer { get; set; }
    }
}
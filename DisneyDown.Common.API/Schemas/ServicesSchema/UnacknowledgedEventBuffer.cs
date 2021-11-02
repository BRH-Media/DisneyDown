using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class UnacknowledgedEventBuffer
    {
        [JsonProperty("maxSize")]
        public long MaxSize { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PlaybackSession
    {
        [JsonProperty("streamSampleInterval")]
        public long StreamSampleInterval { get; set; }
    }
}
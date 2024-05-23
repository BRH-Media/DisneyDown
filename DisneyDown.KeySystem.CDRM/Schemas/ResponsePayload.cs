using Newtonsoft.Json;

namespace DisneyDown.KeySystem.CDRM.Schemas
{
    public class ResponsePayload
    {
        [JsonProperty(@"keys")]
        public ResponsePayloadKey[] Keys { get; set; }

        [JsonProperty(@"error")]
        public string Error { get; set; }
    }
}
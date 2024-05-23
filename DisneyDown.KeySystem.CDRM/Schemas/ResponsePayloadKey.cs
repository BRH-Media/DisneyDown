using Newtonsoft.Json;

namespace DisneyDown.KeySystem.CDRM.Schemas
{
    public class ResponsePayloadKey
    {
        [JsonProperty(@"key")]
        public string Key { get; set; }
    }
}
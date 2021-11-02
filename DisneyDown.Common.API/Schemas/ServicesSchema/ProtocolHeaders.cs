using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ProtocolHeaders
    {
        [JsonProperty("json")]
        public string Json { get; set; }

        [JsonProperty("protobuf")]
        public string Protobuf { get; set; }
    }
}
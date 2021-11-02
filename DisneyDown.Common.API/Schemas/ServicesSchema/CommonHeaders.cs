using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CommonHeaders
    {
        [JsonProperty("X-Application-Version")]
        public string XApplicationVersion { get; set; }

        [JsonProperty("X-BAMSDK-Client-ID")]
        public string XBamsdkClientId { get; set; }

        [JsonProperty("X-BAMSDK-Platform")]
        public string XBamsdkPlatform { get; set; }

        [JsonProperty("X-BAMSDK-Version")]
        public string XBamsdkVersion { get; set; }

        [JsonProperty("X-DSS-Edge-Accept")]
        public string XDssEdgeAccept { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class RegisterChannelHeaders
    {
        [JsonProperty("Authorization")]
        public string Authorization { get; set; }

        [JsonProperty("X-BAMTech-App-Id")]
        public string XBamTechAppId { get; set; }
    }
}
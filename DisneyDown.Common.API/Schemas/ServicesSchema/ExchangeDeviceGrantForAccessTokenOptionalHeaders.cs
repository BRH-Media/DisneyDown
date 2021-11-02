using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ExchangeDeviceGrantForAccessTokenOptionalHeaders
    {
        [JsonProperty("X-BAMTech-Globalization-Version")]
        public string XBamTechGlobalizationVersion { get; set; }
    }
}
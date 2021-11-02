using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class IndigoExtras
    {
        [JsonProperty("platformId")]
        public string PlatformId { get; set; }

        [JsonProperty("setCookie")]
        public bool SetCookie { get; set; }
    }
}
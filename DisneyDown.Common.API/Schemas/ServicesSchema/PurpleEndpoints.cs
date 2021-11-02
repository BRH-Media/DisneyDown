using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PurpleEndpoints
    {
        [JsonProperty("setToken")]
        public SetToken SetToken { get; set; }

        [JsonProperty("setTokenPost")]
        public SetTokenPost SetTokenPost { get; set; }

        [JsonProperty("setTokenRedirect")]
        public SetToken SetTokenRedirect { get; set; }

        [JsonProperty("setTokenRedirectPost")]
        public SetTokenPost SetTokenRedirectPost { get; set; }
    }
}
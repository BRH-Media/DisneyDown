using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas
{
    public class TokenExchangeResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        //deserialiser
        public static TokenExchangeResponse FromJson(string json) => JsonConvert.DeserializeObject<TokenExchangeResponse>(json, Converter.Settings);
    }
}
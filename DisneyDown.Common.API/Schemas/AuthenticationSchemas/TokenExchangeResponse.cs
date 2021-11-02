using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.AuthenticationSchemas
{
    public class TokenExchangeResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        //deserialiser
        public static TokenExchangeResponse FromJson(string json) => JsonConvert.DeserializeObject<TokenExchangeResponse>(json, ApiJsonConverter.Settings);
    }
}
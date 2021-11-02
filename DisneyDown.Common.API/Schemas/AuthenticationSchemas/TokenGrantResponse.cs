using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.AuthenticationSchemas
{
    public class TokenGrantResponse
    {
        [JsonProperty("assertion")]
        public string Assertion { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        //deserialiser
        public static TokenGrantResponse FromJson(string json) => JsonConvert.DeserializeObject<TokenGrantResponse>(json, ApiJsonConverter.Settings);
    }
}
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace DisneyDown.Common.API.Schemas.AuthenticationSchemas
{
    public class BAMIdentityResponse
    {
        [JsonProperty("id_token")]
        public string IdToken { get; set; }

        //deserialiser
        public static BAMIdentityResponse FromJson(string json) => JsonConvert.DeserializeObject<BAMIdentityResponse>(json, ApiJsonConverter.Settings);
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ExternalAuthorization
    {
        [JsonProperty("client")]
        public ExternalAuthorizationClient Client { get; set; }
    }
}
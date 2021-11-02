using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class AmbitiousEndpoints
    {
        [JsonProperty("verifyMediaRights")]
        public Endpoint VerifyMediaRights { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CunningEndpoints
    {
        [JsonProperty("activateBundle")]
        public Endpoint ActivateBundle { get; set; }

        [JsonProperty("activateToken")]
        public Endpoint ActivateToken { get; set; }

        [JsonProperty("getActivationToken")]
        public Endpoint GetActivationToken { get; set; }
    }
}
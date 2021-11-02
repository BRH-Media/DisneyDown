using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ExternalActivation
    {
        [JsonProperty("client")]
        public ExternalActivationClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints3
    {
        [JsonProperty("registerChannel")]
        public RegisterChannel RegisterChannel { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Orchestration
    {
        [JsonProperty("client")]
        public OrchestrationClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Session
    {
        [JsonProperty("client")]
        public SessionClient Client { get; set; }
    }
}
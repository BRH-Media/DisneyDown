using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Socket
    {
        [JsonProperty("client")]
        public SocketClient Client { get; set; }

        [JsonProperty("disabled")]
        public bool Disabled { get; set; }

        [JsonProperty("extras")]
        public SocketExtras Extras { get; set; }
    }
}
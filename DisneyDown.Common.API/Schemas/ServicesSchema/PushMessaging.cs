using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PushMessaging
    {
        [JsonProperty("client")]
        public PushMessagingClient Client { get; set; }
    }
}
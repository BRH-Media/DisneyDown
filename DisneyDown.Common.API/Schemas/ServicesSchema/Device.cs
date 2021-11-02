using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Device
    {
        [JsonProperty("client")]
        public DeviceClient Client { get; set; }
    }
}
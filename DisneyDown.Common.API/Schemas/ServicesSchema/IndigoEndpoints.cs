using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class IndigoEndpoints
    {
        [JsonProperty("createDeviceGrant")]
        public Endpoint CreateDeviceGrant { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class MagentaEndpoints
    {
        [JsonProperty("createLinkGrant")]
        public Endpoint CreateLinkGrant { get; set; }
    }
}
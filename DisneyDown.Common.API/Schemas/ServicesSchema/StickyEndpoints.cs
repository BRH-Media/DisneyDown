using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class StickyEndpoints
    {
        [JsonProperty("createSupportCode")]
        public GetDefaultPaymentMethod CreateSupportCode { get; set; }
    }
}
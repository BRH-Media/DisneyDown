using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Purchase
    {
        [JsonProperty("client")]
        public PurchaseClient Client { get; set; }

        [JsonProperty("extras")]
        public PurchaseExtras Extras { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Endpoints2
    {
        [JsonProperty("redeemPurchases")]
        public Endpoint RedeemPurchases { get; set; }

        [JsonProperty("restorePurchases")]
        public Endpoint RestorePurchases { get; set; }
    }
}
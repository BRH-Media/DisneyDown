using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Invoice
    {
        [JsonProperty("client")]
        public InvoiceClient Client { get; set; }
    }
}
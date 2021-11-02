using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class FriskyEndpoints
    {
        [JsonProperty("getInvoice")]
        public Endpoint GetInvoice { get; set; }

        [JsonProperty("getPaginatedInvoices")]
        public Endpoint GetPaginatedInvoices { get; set; }
    }
}
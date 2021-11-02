using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class PurpleExtras
    {
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("createPaymentMethodRegionalEndpoints")]
        public CreatePaymentMethodRegionalEndpoints CreatePaymentMethodRegionalEndpoints { get; set; }

        [JsonProperty("namespace")]
        public string Namespace { get; set; }

        [JsonProperty("namespaceId")]
        public long NamespaceId { get; set; }
    }
}
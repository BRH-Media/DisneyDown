using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class StickyExtras
    {
        [JsonProperty("connectionPairingEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints ConnectionPairingEndpointMapping { get; set; }

        [JsonProperty("pairedConnectionEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints PairedConnectionEndpointMapping { get; set; }

        [JsonProperty("protocolHeaders")]
        public ProtocolHeaders ProtocolHeaders { get; set; }

        [JsonProperty("regionalEndpointMapping")]
        public CreatePaymentMethodRegionalEndpoints RegionalEndpointMapping { get; set; }

        [JsonProperty("supportedProtocols")]
        public string[] SupportedProtocols { get; set; }
    }
}
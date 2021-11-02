using Newtonsoft.Json;

// ReSharper disable RedundantIfElseBlock
// ReSharper disable IdentifierTypo
// ReSharper disable StringLiteralTypo

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Services
    {
        [JsonProperty("account")]
        public Account Account { get; set; }

        [JsonProperty("adEngine")]
        public AdEngine AdEngine { get; set; }

        [JsonProperty("bamIdentity")]
        public BamIdentity BamIdentity { get; set; }

        [JsonProperty("commerce")]
        public Commerce Commerce { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("customerService")]
        public CustomerService CustomerService { get; set; }

        [JsonProperty("device")]
        public Device Device { get; set; }

        [JsonProperty("drm")]
        public Drm Drm { get; set; }

        [JsonProperty("eligibility")]
        public Eligibility Eligibility { get; set; }

        [JsonProperty("entitlement")]
        public Entitlement Entitlement { get; set; }

        [JsonProperty("externalActivation")]
        public ExternalActivation ExternalActivation { get; set; }

        [JsonProperty("externalAuthorization")]
        public ExternalAuthorization ExternalAuthorization { get; set; }

        [JsonProperty("invoice")]
        public Invoice Invoice { get; set; }

        [JsonProperty("media")]
        public Media Media { get; set; }

        [JsonProperty("orchestration")]
        public Orchestration Orchestration { get; set; }

        [JsonProperty("paywall")]
        public Paywall Paywall { get; set; }

        [JsonProperty("purchase")]
        public Purchase Purchase { get; set; }

        [JsonProperty("pushMessaging")]
        public PushMessaging PushMessaging { get; set; }

        [JsonProperty("session")]
        public Session Session { get; set; }

        [JsonProperty("socket")]
        public Socket Socket { get; set; }

        [JsonProperty("subscription")]
        public Subscription Subscription { get; set; }

        [JsonProperty("telemetry")]
        public Telemetry Telemetry { get; set; }

        [JsonProperty("token")]
        public Token Token { get; set; }
    }
}
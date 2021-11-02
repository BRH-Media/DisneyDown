using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class IndecentEndpoints
    {
        [JsonProperty("fairPlayCapability")]
        public Endpoint FairPlayCapability { get; set; }

        [JsonProperty("fairPlayCertificate")]
        public SetToken FairPlayCertificate { get; set; }

        [JsonProperty("fairPlayLicense")]
        public Endpoint FairPlayLicense { get; set; }

        [JsonProperty("fairPlayLinearLicense")]
        public Endpoint FairPlayLinearLicense { get; set; }

        [JsonProperty("nagraCertificate")]
        public SetToken NagraCertificate { get; set; }

        [JsonProperty("nagraLicense")]
        public Endpoint NagraLicense { get; set; }

        [JsonProperty("nagraLinearLicense")]
        public Endpoint NagraLinearLicense { get; set; }

        [JsonProperty("offlineFairPlayLicense")]
        public Endpoint OfflineFairPlayLicense { get; set; }

        [JsonProperty("offlineFairPlayLicenseCheck")]
        public Endpoint OfflineFairPlayLicenseCheck { get; set; }

        [JsonProperty("offlineFairPlayLicenseRelease")]
        public Endpoint OfflineFairPlayLicenseRelease { get; set; }

        [JsonProperty("offlineFairPlayLicenseRenew")]
        public Endpoint OfflineFairPlayLicenseRenew { get; set; }

        [JsonProperty("offlineWidevineLicense")]
        public SetTokenPost OfflineWidevineLicense { get; set; }

        [JsonProperty("offlineWidevineLicenseCheck")]
        public Endpoint OfflineWidevineLicenseCheck { get; set; }

        [JsonProperty("offlineWidevineLicenseRelease")]
        public SetTokenPost OfflineWidevineLicenseRelease { get; set; }

        [JsonProperty("offlineWidevineLicenseRenew")]
        public SetTokenPost OfflineWidevineLicenseRenew { get; set; }

        [JsonProperty("playReadyCapability")]
        public SubmitMercadoPayment PlayReadyCapability { get; set; }

        [JsonProperty("playReadyLicense")]
        public SubmitMercadoPayment PlayReadyLicense { get; set; }

        [JsonProperty("playReadyLinearLicense")]
        public SubmitMercadoPayment PlayReadyLinearLicense { get; set; }

        [JsonProperty("silkKey")]
        public GetDefaultPaymentMethod SilkKey { get; set; }

        [JsonProperty("widevineCapability")]
        public SetTokenPost WidevineCapability { get; set; }

        [JsonProperty("widevineCertificate")]
        public SetToken WidevineCertificate { get; set; }

        [JsonProperty("widevineLicense")]
        public SetTokenPost WidevineLicense { get; set; }

        [JsonProperty("widevineLinearLicense")]
        public SetTokenPost WidevineLinearLicense { get; set; }
    }
}
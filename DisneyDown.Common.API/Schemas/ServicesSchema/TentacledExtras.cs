using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TentacledExtras
    {
        [JsonProperty("drmLicenseEndpoints")]
        public AdTargeting DrmLicenseEndpoints { get; set; }

        [JsonProperty("licenseEndpoints")]
        public LicenseEndpoints LicenseEndpoints { get; set; }
    }
}
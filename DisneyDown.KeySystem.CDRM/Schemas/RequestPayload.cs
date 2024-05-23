using Newtonsoft.Json;

namespace DisneyDown.KeySystem.CDRM.Schemas
{
    public class RequestPayload
    {
        [JsonProperty(@"license")]
        public string LicenseServerUrl { get; set; } = @"https://disney.playback.edge.bamgrid.com/widevine/v1/obtain-license";

        [JsonProperty(@"headers")]
        public string DelimitedPostHeaders { get; set; } = @"";

        [JsonProperty(@"pssh")]
        public string PsshB64 { get; set; } = @"";

        [JsonProperty(@"proxy")]
        public string Proxy { get; set; } = @"";

        [JsonProperty(@"buildInfo")]
        public string CdmBlob { get; set; } = @"";

        [JsonProperty(@"cache")]
        public bool Cache { get; set; } = true;
    }
}
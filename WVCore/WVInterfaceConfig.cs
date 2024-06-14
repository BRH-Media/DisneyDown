using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace WVCore
{
    public class WVInterfaceConfig
    {
        public string LicenceClient { get; set; } = WVHttpUtil.DefaultUserAgent;
        public string LicenceServer { get; set; } = @"";
        public string LicenceCertificate { get; set; } = @"";
        public string LicenceAuthorization { get; set; } = @"";
        public Dictionary<string, string> LicenceHeaders { get; set; } = new Dictionary<string, string>
        {
            {"Priority", "u=4"},
            {"Referer", ""},
            {"Host", ""},
            {"Origin", ""},
            {"Connection", "keep-alive"},
            {"Accept", "*/*"},
            {"Accept-Encoding", "gzip, deflate, br, zstd"},
            {"Accept-Language", "en-US,en;q=0.5"},
            {"Sec-Fetch-Dest", "empty"},
            {"Sec-Fetch-Mode", "cors"},
            {"Sec-Fetch-Site", "cross-site"},
            {"TE", "trailers"},
        };

        public string ToJson(bool indented = true)
            => JsonConvert.SerializeObject(this, indented ? Formatting.Indented : Formatting.None);

        public static WVInterfaceConfig FromJson(string json)
        {
            var obj = JsonConvert.DeserializeObject<WVInterfaceConfig>(json);
            return obj ?? new WVInterfaceConfig();
        }
    }
}
using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace DisneyDown.Common.API.Structures.RequestPayloads
{
    public class TokenExchangeRequestPayload
    {
        [JsonProperty("refresh_token")]
        public string refresh_token { get; set; } = @"";

        [JsonProperty("grant_type")]
        public string grant_type { get; set; } = @"";

        [JsonProperty("platform")]
        public string platform { get; set; } = @"";

        [JsonProperty("subject_token")]
        public string subject_token { get; set; } = @"";

        [JsonProperty("subject_token_type")]
        public string subject_token_type { get; set; } = @"";
    }
}
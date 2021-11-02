using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class TokenExtras
    {
        [JsonProperty("autoRefreshRetryPolicy")]
        public RetryPolicy AutoRefreshRetryPolicy { get; set; }

        [JsonProperty("refreshThreshold")]
        public double RefreshThreshold { get; set; }

        [JsonProperty("subjectTokenTypes")]
        public SubjectTokenTypes SubjectTokenTypes { get; set; }
    }
}
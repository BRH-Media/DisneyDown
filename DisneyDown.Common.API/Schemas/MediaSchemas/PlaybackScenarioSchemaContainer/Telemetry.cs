using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Telemetry
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("cdn_policy_id")]
        public string CdnPolicyId { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("mediaId")]
        public Guid MediaId { get; set; }
    }
}
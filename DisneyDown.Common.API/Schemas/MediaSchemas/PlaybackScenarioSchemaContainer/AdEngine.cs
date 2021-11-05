using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class AdEngine
    {
        [JsonProperty("cdn")]
        public string Cdn { get; set; }

        [JsonProperty("corigin")]
        public string Corigin { get; set; }

        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }
    }
}
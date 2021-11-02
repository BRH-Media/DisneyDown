using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class FluffyTelemetry
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }

        [JsonProperty("mediaId")]
        public Guid MediaId { get; set; }
    }
}
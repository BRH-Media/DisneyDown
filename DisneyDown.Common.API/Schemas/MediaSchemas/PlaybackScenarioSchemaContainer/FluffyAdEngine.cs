using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class FluffyAdEngine
    {
        [JsonProperty("fguid")]
        public Guid Fguid { get; set; }
    }
}
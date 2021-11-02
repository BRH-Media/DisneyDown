using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public class Complete
    {
        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("tracking")]
        public CompleteTracking Tracking { get; set; }
    }
}
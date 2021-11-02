using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer
{
    public partial class Playhead
    {
        [JsonProperty("position")]
        public long Position { get; set; }

        [JsonProperty("last_modified")]
        public string LastModified { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("content_id")]
        public Guid ContentId { get; set; }
    }
}
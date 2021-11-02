using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class MilestoneTime
    {
        [JsonProperty("startMillis")]
        public long StartMillis { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class PurpleDescription
    {
        [JsonProperty("medium")]
        public PurpleBrief Medium { get; set; }

        [JsonProperty("brief")]
        public PurpleBrief Brief { get; set; }

        [JsonProperty("full")]
        public PurpleBrief Full { get; set; }
    }
}
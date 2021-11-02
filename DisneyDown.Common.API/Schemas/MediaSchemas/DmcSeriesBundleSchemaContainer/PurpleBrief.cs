using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class PurpleBrief
    {
        [JsonProperty("program", NullValueHandling = NullValueHandling.Ignore)]
        public BriefSeries Program { get; set; }

        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public BriefSeries Series { get; set; }
    }
}
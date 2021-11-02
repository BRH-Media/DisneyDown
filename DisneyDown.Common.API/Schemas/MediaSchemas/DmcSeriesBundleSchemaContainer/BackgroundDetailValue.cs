using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class BackgroundDetailValue
    {
        [JsonProperty("series", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundDetailSeries Series { get; set; }

        [JsonProperty("program", NullValueHandling = NullValueHandling.Ignore)]
        public BackgroundDetailSeries Program { get; set; }
    }
}
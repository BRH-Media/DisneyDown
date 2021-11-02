using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class BackgroundValue
    {
        [JsonProperty("program")]
        public BackgroundDetailSeries Program { get; set; }
    }
}
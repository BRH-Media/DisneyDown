using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class BackgroundDetail
    {
        [JsonProperty("series")]
        public BackgroundDetailSeries Series { get; set; }
    }
}
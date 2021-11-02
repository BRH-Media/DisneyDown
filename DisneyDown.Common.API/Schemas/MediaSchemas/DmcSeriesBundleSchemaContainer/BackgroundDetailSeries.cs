using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class BackgroundDetailSeries
    {
        [JsonProperty("default")]
        public PurpleDefault Default { get; set; }
    }
}
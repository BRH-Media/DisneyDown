using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Data
    {
        [JsonProperty("DmcSeriesBundle")]
        public DmcSeriesBundle DmcSeriesBundle { get; set; }
    }
}
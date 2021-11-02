using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class DmcSeriesBundle
    {
        [JsonProperty("episodes")]
        public Episodes Episodes { get; set; }

        [JsonProperty("extras")]
        public Episodes Extras { get; set; }

        [JsonProperty("promoLabels")]
        public object[] PromoLabels { get; set; }

        [JsonProperty("related")]
        public Related Related { get; set; }

        [JsonProperty("seasons")]
        public Seasons Seasons { get; set; }

        [JsonProperty("series")]
        public DmcSeriesBundleSeries Series { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class FluffyBrief
    {
        [JsonProperty("series")]
        public BriefSeries Series { get; set; }
    }
}
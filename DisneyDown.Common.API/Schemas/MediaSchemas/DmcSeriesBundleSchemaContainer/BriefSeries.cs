using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class BriefSeries
    {
        [JsonProperty("default")]
        public FluffyDefault Default { get; set; }
    }
}
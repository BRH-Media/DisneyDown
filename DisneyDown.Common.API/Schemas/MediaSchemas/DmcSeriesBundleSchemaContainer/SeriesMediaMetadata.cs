using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class SeriesMediaMetadata
    {
        [JsonProperty("facets")]
        public FluffyFacet[] Facets { get; set; }
    }
}
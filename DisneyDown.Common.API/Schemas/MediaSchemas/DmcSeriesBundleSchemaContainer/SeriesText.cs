using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class SeriesText
    {
        [JsonProperty("description")]
        public FluffyDescription Description { get; set; }

        [JsonProperty("title")]
        public FluffyTitle Title { get; set; }
    }
}
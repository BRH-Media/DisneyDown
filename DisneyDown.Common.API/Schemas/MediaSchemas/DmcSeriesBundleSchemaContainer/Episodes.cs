using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Episodes
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("videos")]
        public Video[] Videos { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Seasons
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("seasons")]
        public Season[] SeasonsSeasons { get; set; }
    }
}
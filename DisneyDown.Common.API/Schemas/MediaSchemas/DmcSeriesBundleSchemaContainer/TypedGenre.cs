using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class TypedGenre
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("partnerId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PartnerId { get; set; }
    }
}
using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class TypedGenre
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("partnerId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PartnerId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
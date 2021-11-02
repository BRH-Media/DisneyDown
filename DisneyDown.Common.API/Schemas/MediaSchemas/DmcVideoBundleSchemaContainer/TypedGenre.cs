using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class TypedGenre
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
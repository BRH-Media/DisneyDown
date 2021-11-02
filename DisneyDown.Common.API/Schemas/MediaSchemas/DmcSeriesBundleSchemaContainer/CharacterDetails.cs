using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class CharacterDetails
    {
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("characterId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CharacterId { get; set; }
    }
}
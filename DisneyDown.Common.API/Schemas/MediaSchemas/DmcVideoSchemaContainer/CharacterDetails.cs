using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class CharacterDetails
    {
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("characterId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long CharacterId { get; set; }

        [JsonProperty("language")]
        public object Language { get; set; }
    }
}
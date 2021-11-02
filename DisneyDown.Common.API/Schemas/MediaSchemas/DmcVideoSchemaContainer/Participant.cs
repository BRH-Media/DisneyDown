using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Participant
    {
        [JsonProperty("characterDetails")]
        public CharacterDetails CharacterDetails { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("participantId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long ParticipantId { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("sortName")]
        public string SortName { get; set; }
    }
}
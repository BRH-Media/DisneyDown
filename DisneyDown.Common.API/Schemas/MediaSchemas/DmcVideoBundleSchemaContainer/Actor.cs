using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Actor
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

        [JsonProperty("sortName")]
        public string SortName { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Participant
    {
        [JsonProperty("Actor")]
        public Actor[] Actor { get; set; }

        [JsonProperty("Director")]
        public Actor[] Director { get; set; }

        [JsonProperty("Producer")]
        public Actor[] Producer { get; set; }
    }
}
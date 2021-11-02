using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Participant
    {
        [JsonProperty("Actor")]
        public Actor[] Actor { get; set; }

        [JsonProperty("Director")]
        public Actor[] Director { get; set; }

        [JsonProperty("Producer")]
        public Actor[] Producer { get; set; }
    }
}
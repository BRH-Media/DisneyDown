using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Participant
    {
        [JsonProperty("Actor")]
        public Actor[] Actor { get; set; }

        [JsonProperty("Created By")]
        public Actor[] CreatedBy { get; set; }
    }
}
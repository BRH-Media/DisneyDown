using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Text
    {
        [JsonProperty("description")]
        public Description Description { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }
    }
}
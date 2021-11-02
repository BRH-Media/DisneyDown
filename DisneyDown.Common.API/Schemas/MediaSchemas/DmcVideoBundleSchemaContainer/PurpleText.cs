using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class PurpleText
    {
        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("description")]
        public PurpleDescription Description { get; set; }
    }
}
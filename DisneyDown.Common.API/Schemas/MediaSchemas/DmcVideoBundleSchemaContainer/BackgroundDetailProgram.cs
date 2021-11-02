using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class BackgroundDetailProgram
    {
        [JsonProperty("default")]
        public PurpleDefault Default { get; set; }
    }
}
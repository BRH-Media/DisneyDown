using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class FluffyBackgroundDetail
    {
        [JsonProperty("program")]
        public BackgroundDetailProgram Program { get; set; }
    }
}
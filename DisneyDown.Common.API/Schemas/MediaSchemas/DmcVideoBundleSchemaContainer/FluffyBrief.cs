using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class FluffyBrief
    {
        [JsonProperty("program")]
        public BriefProgram Program { get; set; }
    }
}
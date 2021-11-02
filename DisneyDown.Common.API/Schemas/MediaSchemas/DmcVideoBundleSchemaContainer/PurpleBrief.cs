using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class PurpleBrief
    {
        [JsonProperty("series")]
        public BriefProgram Series { get; set; }
    }
}
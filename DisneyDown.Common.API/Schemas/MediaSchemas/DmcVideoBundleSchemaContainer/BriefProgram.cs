using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class BriefProgram
    {
        [JsonProperty("default")]
        public FluffyDefault Default { get; set; }
    }
}
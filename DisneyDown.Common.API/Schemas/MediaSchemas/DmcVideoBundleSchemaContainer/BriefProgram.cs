using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class BriefProgram
    {
        [JsonProperty("default")]
        public FluffyDefault Default { get; set; }
    }
}
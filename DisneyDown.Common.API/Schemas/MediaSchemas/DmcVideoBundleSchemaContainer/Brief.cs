using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Brief
    {
        [JsonProperty("program")]
        public BriefProgram Program { get; set; }
    }
}
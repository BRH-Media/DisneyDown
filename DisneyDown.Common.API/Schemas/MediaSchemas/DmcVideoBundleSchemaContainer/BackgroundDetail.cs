using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class BackgroundDetail
    {
        [JsonProperty("program")]
        public BackgroundDetailProgram Program { get; set; }
    }
}
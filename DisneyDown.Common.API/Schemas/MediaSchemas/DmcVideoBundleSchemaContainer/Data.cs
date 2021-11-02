using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Data
    {
        [JsonProperty("DmcVideoBundle")]
        public DmcVideoBundle DmcVideoBundle { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Extras
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("videos")]
        public VideoElement[] Videos { get; set; }
    }
}
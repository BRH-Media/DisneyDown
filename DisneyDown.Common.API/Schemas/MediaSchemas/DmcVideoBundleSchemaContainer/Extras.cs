using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Extras
    {
        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("videos")]
        public object[] Videos { get; set; }
    }
}
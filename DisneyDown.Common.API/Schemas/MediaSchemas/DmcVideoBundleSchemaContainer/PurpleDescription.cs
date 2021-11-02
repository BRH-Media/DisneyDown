using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class PurpleDescription
    {
        [JsonProperty("medium")]
        public Brief Medium { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class FluffyDescription
    {
        [JsonProperty("medium")]
        public Brief Medium { get; set; }

        [JsonProperty("brief")]
        public Brief Brief { get; set; }

        [JsonProperty("full")]
        public Brief Full { get; set; }
    }
}
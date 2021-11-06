using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Thumbnail
    {
        [JsonProperty("1.78")]
        public Thumbnail178 The178 { get; set; }
    }
}
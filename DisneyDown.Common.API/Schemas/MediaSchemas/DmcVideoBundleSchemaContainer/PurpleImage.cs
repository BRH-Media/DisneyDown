using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class PurpleImage
    {
        [JsonProperty("thumbnail")]
        public BackgroundUpNext Thumbnail { get; set; }
    }
}
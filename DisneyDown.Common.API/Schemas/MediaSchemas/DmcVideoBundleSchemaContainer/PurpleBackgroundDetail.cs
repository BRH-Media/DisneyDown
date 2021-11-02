using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class PurpleBackgroundDetail
    {
        [JsonProperty("series")]
        public BackgroundDetailProgram Series { get; set; }
    }
}
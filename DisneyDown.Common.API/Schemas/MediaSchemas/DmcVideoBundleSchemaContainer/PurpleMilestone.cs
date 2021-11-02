using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class PurpleMilestone
    {
        [JsonProperty("up_next")]
        public UpNext[] UpNext { get; set; }
    }
}
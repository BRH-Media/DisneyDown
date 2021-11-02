using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class ItemMediaRights
    {
        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class MediaRights
    {
        [JsonProperty("violations")]
        public object[] Violations { get; set; }

        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }

        [JsonProperty("rewind")]
        public bool Rewind { get; set; }
    }
}
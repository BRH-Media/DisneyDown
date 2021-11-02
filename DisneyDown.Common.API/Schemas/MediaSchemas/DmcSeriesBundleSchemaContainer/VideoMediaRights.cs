using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class VideoMediaRights
    {
        [JsonProperty("violations", NullValueHandling = NullValueHandling.Ignore)]
        public object[] Violations { get; set; }

        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }

        [JsonProperty("rewind", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Rewind { get; set; }
    }
}
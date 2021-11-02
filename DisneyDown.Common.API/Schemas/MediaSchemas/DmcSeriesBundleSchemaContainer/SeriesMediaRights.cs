using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class SeriesMediaRights
    {
        [JsonProperty("downloadBlocked")]
        public bool DownloadBlocked { get; set; }

        [JsonProperty("pconBlocked")]
        public bool PconBlocked { get; set; }
    }
}
using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Release
    {
        [JsonProperty("releaseDate")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("releaseOrg")]
        public object ReleaseOrg { get; set; }

        [JsonProperty("releaseType")]
        public string ReleaseType { get; set; }

        [JsonProperty("releaseYear")]
        public long ReleaseYear { get; set; }

        [JsonProperty("territory")]
        public object Territory { get; set; }
    }
}
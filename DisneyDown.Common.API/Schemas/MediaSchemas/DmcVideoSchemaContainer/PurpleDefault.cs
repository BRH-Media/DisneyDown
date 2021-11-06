using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class PurpleDefault
    {
        [JsonProperty("masterId")]
        public string MasterId { get; set; }

        [JsonProperty("masterWidth")]
        public long MasterWidth { get; set; }

        [JsonProperty("masterHeight")]
        public long MasterHeight { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}
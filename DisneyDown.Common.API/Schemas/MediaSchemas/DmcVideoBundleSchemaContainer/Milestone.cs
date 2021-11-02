using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Milestone
    {
        [JsonProperty("FFOC")]
        public Ffec[] Ffoc { get; set; }

        [JsonProperty("FFEC")]
        public Ffec[] Ffec { get; set; }

        [JsonProperty("up_next")]
        public Ffec[] UpNext { get; set; }

        [JsonProperty("LFOC")]
        public Ffec[] Lfoc { get; set; }
    }
}
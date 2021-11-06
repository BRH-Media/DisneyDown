using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Milestone
    {
        [JsonProperty("intro_start")]
        public Ffec[] IntroStart { get; set; }

        [JsonProperty("FFEI")]
        public Ffec[] Ffei { get; set; }

        [JsonProperty("intro_end")]
        public Ffec[] IntroEnd { get; set; }

        [JsonProperty("FFEC")]
        public Ffec[] Ffec { get; set; }

        [JsonProperty("up_next")]
        public Ffec[] UpNext { get; set; }
    }
}
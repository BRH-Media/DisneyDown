using System.Collections.Generic;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class VideoImage
    {
        [JsonProperty("tile")]
        public Dictionary<string, FluffyBackgroundDetail> Tile { get; set; }

        [JsonProperty("title_treatment_centered")]
        public FluffyBackgroundUpNext TitleTreatmentCentered { get; set; }

        [JsonProperty("title_treatment")]
        public FluffyBackgroundUpNext TitleTreatment { get; set; }

        [JsonProperty("background_up_next")]
        public FluffyBackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, FluffyBackgroundDetail> BackgroundDetails { get; set; }
    }
}
using System.Collections.Generic;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class ItemImage
    {
        [JsonProperty("tile")]
        public Dictionary<string, PurpleBackgroundDetail> Tile { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, PurpleBackgroundDetail> BackgroundDetails { get; set; }

        [JsonProperty("title_treatment")]
        public PurpleBackgroundUpNext TitleTreatment { get; set; }

        [JsonProperty("title_treatment_centered", NullValueHandling = NullValueHandling.Ignore)]
        public PurpleBackgroundUpNext TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next")]
        public PurpleBackgroundUpNext BackgroundUpNext { get; set; }
    }
}
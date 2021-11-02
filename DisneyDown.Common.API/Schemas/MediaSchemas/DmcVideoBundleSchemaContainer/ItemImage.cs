using System.Collections.Generic;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class ItemImage
    {
        [JsonProperty("title_treatment_centered")]
        public BackgroundUpNext TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next")]
        public BackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("title_treatment")]
        public BackgroundUpNext TitleTreatment { get; set; }

        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetail> Tile { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, BackgroundDetail> BackgroundDetails { get; set; }
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class ItemImage
    {
        [JsonProperty("title_treatment")]
        public TitleTreatment TitleTreatment { get; set; }

        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetailValue> Tile { get; set; }

        [JsonProperty("background", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, BackgroundValue> Background { get; set; }

        [JsonProperty("title_treatment_centered")]
        public TitleTreatment TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next", NullValueHandling = NullValueHandling.Ignore)]
        public TitleTreatment BackgroundUpNext { get; set; }

        [JsonProperty("background_details", NullValueHandling = NullValueHandling.Ignore)]
        public Dictionary<string, BackgroundDetailValue> BackgroundDetails { get; set; }
    }
}
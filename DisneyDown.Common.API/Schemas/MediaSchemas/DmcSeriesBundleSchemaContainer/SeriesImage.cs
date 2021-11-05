using Newtonsoft.Json;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class SeriesImage
    {
        [JsonProperty("tile")]
        public Dictionary<string, BackgroundDetail> Tile { get; set; }

        [JsonProperty("title_treatment")]
        public BackgroundUpNext TitleTreatment { get; set; }

        [JsonProperty("title_treatment_centered")]
        public BackgroundUpNext TitleTreatmentCentered { get; set; }

        [JsonProperty("background_up_next")]
        public BackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, BackgroundDetail> BackgroundDetails { get; set; }
    }
}
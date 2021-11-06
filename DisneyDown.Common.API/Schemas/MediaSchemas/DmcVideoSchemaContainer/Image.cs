using Newtonsoft.Json;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Image
    {
        [JsonProperty("tile")]
        public Dictionary<string, The178_Value> Tile { get; set; }

        [JsonProperty("title_treatment_layer")]
        public Dictionary<string, The178_Value> TitleTreatmentLayer { get; set; }

        [JsonProperty("title_treatment")]
        public Dictionary<string, The178_Value> TitleTreatment { get; set; }

        [JsonProperty("tile_partner")]
        public Dictionary<string, The178_Value> TilePartner { get; set; }

        [JsonProperty("title_treatment_mono")]
        public TitleTreatmentMono TitleTreatmentMono { get; set; }

        [JsonProperty("title_treatment_centered")]
        public Background TitleTreatmentCentered { get; set; }

        [JsonProperty("hero_partner")]
        public Dictionary<string, The178_Value> HeroPartner { get; set; }

        [JsonProperty("background")]
        public Background Background { get; set; }

        [JsonProperty("hero")]
        public Dictionary<string, The178_Value> Hero { get; set; }

        [JsonProperty("background_up_next")]
        public BackgroundUpNext BackgroundUpNext { get; set; }

        [JsonProperty("hero_tile")]
        public Dictionary<string, The178_Value> HeroTile { get; set; }

        [JsonProperty("background_details")]
        public Dictionary<string, The178_Value> BackgroundDetails { get; set; }

        [JsonProperty("hero_collection")]
        public Background HeroCollection { get; set; }

        [JsonProperty("thumbnail")]
        public Thumbnail Thumbnail { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class MischievousEndpoints
    {
        [JsonProperty("bifThumbnail")]
        public Thumbnail BifThumbnail { get; set; }

        [JsonProperty("bifThumbnails")]
        public SetToken BifThumbnails { get; set; }

        [JsonProperty("bookmarks")]
        public GetDefaultPaymentMethod Bookmarks { get; set; }

        [JsonProperty("key")]
        public GetDefaultPaymentMethod Key { get; set; }

        [JsonProperty("mediaPayload")]
        public MediaPayload MediaPayload { get; set; }

        [JsonProperty("playbackCookie")]
        public SetToken PlaybackCookie { get; set; }

        [JsonProperty("postMediaPayload")]
        public SubmitMercadoPayment PostMediaPayload { get; set; }

        [JsonProperty("spriteSheetThumbnail")]
        public Thumbnail SpriteSheetThumbnail { get; set; }

        [JsonProperty("spriteSheetThumbnails")]
        public SetToken SpriteSheetThumbnails { get; set; }
    }
}
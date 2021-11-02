using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Item
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("image")]
        public ItemImage Image { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("text")]
        public ItemText Text { get; set; }

        [JsonProperty("textExperienceId")]
        public Guid TextExperienceId { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("mediaRights")]
        public ItemMediaRights MediaRights { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("videoArt")]
        public VideoArt[] VideoArt { get; set; }
    }
}
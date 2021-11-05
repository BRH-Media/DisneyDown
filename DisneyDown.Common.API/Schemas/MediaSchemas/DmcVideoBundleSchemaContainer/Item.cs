using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class Item
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public object EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public object EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public object EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("groups")]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle")]
        public string InternalTitle { get; set; }

        [JsonProperty("image")]
        public ItemImage Image { get; set; }

        [JsonProperty("mediaMetadata")]
        public ItemMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public MediaRights MediaRights { get; set; }

        [JsonProperty("league")]
        public object League { get; set; }

        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("programType")]
        public string ProgramType { get; set; }

        [JsonProperty("seasonId")]
        public object SeasonId { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public object SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public object SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public object SeriesType { get; set; }

        [JsonProperty("sport")]
        public object Sport { get; set; }

        [JsonProperty("text")]
        public ItemText Text { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("targetLanguage")]
        public string TargetLanguage { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("videoArt")]
        public VideoArt[] VideoArt { get; set; }

        [JsonProperty("videoId")]
        public Guid VideoId { get; set; }
    }
}
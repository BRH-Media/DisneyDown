using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Item
    {
        [JsonProperty("badging")]
        public object Badging { get; set; }

        [JsonProperty("callToAction")]
        public object CallToAction { get; set; }

        [JsonProperty("contentId")]
        public Guid ContentId { get; set; }

        [JsonProperty("contentType", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        [JsonProperty("currentAvailability")]
        public CurrentAvailability CurrentAvailability { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public object EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public object EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("family", NullValueHandling = NullValueHandling.Ignore)]
        public Family Family { get; set; }

        [JsonProperty("groups", NullValueHandling = NullValueHandling.Ignore)]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalTitle { get; set; }

        [JsonProperty("image")]
        public ItemImage Image { get; set; }

        [JsonProperty("mediaMetadata", NullValueHandling = NullValueHandling.Ignore)]
        public ItemMediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public VideoMediaRights MediaRights { get; set; }

        [JsonProperty("originalLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalLanguage { get; set; }

        [JsonProperty("programId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? ProgramId { get; set; }

        [JsonProperty("programType", NullValueHandling = NullValueHandling.Ignore)]
        public string ProgramType { get; set; }

        [JsonProperty("seasonId")]
        public object SeasonId { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public object SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid? SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("text")]
        public VideoText Text { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("targetLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string TargetLanguage { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("videoArt")]
        public VideoArt[] VideoArt { get; set; }

        [JsonProperty("videoId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? VideoId { get; set; }

        [JsonProperty("textExperienceId", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? TextExperienceId { get; set; }
    }
}
using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public class Video
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
        public string EncodedSeriesId { get; set; }

        [JsonProperty("episodeNumber")]
        public object EpisodeNumber { get; set; }

        [JsonProperty("episodeSequenceNumber")]
        public long EpisodeSequenceNumber { get; set; }

        [JsonProperty("episodeSeriesSequenceNumber")]
        public long EpisodeSeriesSequenceNumber { get; set; }

        [JsonProperty("event")]
        public object Event { get; set; }

        [JsonProperty("family")]
        public Family Family { get; set; }

        [JsonProperty("groups")]
        public Group[] Groups { get; set; }

        [JsonProperty("internalTitle")]
        public string InternalTitle { get; set; }

        [JsonProperty("images")]
        public Image[] Images { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaMetadata")]
        public MediaMetadata MediaMetadata { get; set; }

        [JsonProperty("mediaRights")]
        public MediaRights MediaRights { get; set; }

        [JsonProperty("meta")]
        public object Meta { get; set; }

        [JsonProperty("milestones")]
        public Milestone[] Milestones { get; set; }

        [JsonProperty("originalLanguage")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("participants")]
        public Participant[] Participants { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("programType")]
        public string ProgramType { get; set; }

        [JsonProperty("ratings")]
        public Rating[] Ratings { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("seasonId")]
        public Guid SeasonId { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public long SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("targetLanguage")]
        public string TargetLanguage { get; set; }

        [JsonProperty("texts")]
        public Text[] Texts { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typedGenres")]
        public object[] TypedGenres { get; set; }

        [JsonProperty("videoArt")]
        public object[] VideoArt { get; set; }

        [JsonProperty("videoId")]
        public Guid VideoId { get; set; }
    }
}
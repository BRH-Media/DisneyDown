using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Season
    {
        [JsonProperty("downloadableEpisodes")]
        public Guid[] DownloadableEpisodes { get; set; }

        [JsonProperty("episodes_meta")]
        public Meta EpisodesMeta { get; set; }

        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("labels")]
        public object[] Labels { get; set; }

        [JsonProperty("mediaRights")]
        public VideoMediaRights MediaRights { get; set; }

        [JsonProperty("releases")]
        public Release[] Releases { get; set; }

        [JsonProperty("seasonId")]
        public Guid SeasonId { get; set; }

        [JsonProperty("seasonNumber")]
        public object SeasonNumber { get; set; }

        [JsonProperty("seasonSequenceNumber")]
        public long SeasonSequenceNumber { get; set; }

        [JsonProperty("seriesId")]
        public Guid SeriesId { get; set; }

        [JsonProperty("seriesType")]
        public string SeriesType { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("text")]
        public SeasonText Text { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
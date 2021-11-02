using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class ParentRef
    {
        [JsonProperty("encodedSeriesId")]
        public string EncodedSeriesId { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("seasonId")]
        public Guid? SeasonId { get; set; }

        [JsonProperty("seriesId")]
        public Guid? SeriesId { get; set; }
    }
}
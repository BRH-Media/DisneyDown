using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class ParentRef
    {
        [JsonProperty("encodedSeriesId")]
        public object EncodedSeriesId { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }

        [JsonProperty("seasonId")]
        public object SeasonId { get; set; }

        [JsonProperty("seriesId")]
        public object SeriesId { get; set; }
    }
}
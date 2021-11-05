using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public partial class ParentRef
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
using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer
{
    public partial class Ffec
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("milestoneTime")]
        public MilestoneTime[] MilestoneTime { get; set; }
    }
}
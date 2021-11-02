using System;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Ffec
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("milestoneTime")]
        public MilestoneTime[] MilestoneTime { get; set; }
    }
}
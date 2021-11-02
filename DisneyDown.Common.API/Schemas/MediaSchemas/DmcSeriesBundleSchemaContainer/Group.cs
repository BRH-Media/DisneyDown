using DisneyDown.Common.API.Schemas.ServicesSchema;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer
{
    public class Group
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("partnerGroupId")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long PartnerGroupId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
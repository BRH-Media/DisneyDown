using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer
{
    public class Rating
    {
        [JsonProperty("advisories")]
        public object[] Advisories { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("filingNumber")]
        public object FilingNumber { get; set; }

        [JsonProperty("system")]
        public string System { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
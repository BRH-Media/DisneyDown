using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class FluffyExtras
    {
        [JsonProperty("urlSizeLimit")]
        public long UrlSizeLimit { get; set; }
    }
}
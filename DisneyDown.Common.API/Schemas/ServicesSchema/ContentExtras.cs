using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class ContentExtras
    {
        [JsonProperty("queryIdDefaults")]
        public QueryIdDefaults QueryIdDefaults { get; set; }
    }
}
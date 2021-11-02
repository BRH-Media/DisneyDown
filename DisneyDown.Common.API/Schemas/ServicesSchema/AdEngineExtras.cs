using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class AdEngineExtras
    {
        [JsonProperty("adTargeting")]
        public AdTargeting AdTargeting { get; set; }
    }
}
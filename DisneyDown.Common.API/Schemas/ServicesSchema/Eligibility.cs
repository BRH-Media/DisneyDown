using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Eligibility
    {
        [JsonProperty("client")]
        public EligibilityClient Client { get; set; }
    }
}
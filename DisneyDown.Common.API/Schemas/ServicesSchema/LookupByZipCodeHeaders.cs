using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class LookupByZipCodeHeaders
    {
        [JsonProperty("Accept")]
        public string Accept { get; set; }
    }
}
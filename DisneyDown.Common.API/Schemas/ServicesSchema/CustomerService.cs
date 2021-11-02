using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class CustomerService
    {
        [JsonProperty("client")]
        public CustomerServiceClient Client { get; set; }
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Structures
{
    public class ApiConfigurationCredentials
    {
        [JsonProperty(@"email")]
        public string Email { get; set; } = @"";

        [JsonProperty(@"password")]
        public string Password { get; set; } = @"";
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.KeySystem.Schemas.UserSchema
{
    public class User
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("isAdmin")]
        public bool IsAdmin { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; } = @"";

        [JsonProperty("lastName")]
        public string LastName { get; set; } = @"";
    }
}
using Newtonsoft.Json;

namespace DisneyDown.Common.KeySystem.Schemas.UserSchema
{
    public class UserResponse
    {
        [JsonProperty(@"response")]
        public UserResponseContents Response { get; set; }
    }
}
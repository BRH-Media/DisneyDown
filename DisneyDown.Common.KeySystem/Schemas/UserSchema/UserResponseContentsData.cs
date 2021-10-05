using Newtonsoft.Json;

namespace DisneyDown.Common.KeySystem.Schemas.UserSchema
{
    public class UserResponseContentsData
    {
        [JsonProperty(@"userInformation")]
        public User UserInformation { get; set; } = new User();
    }
}
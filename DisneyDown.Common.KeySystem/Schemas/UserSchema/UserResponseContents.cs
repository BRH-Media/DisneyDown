using Newtonsoft.Json;

namespace DisneyDown.Common.KeySystem.Schemas.UserSchema
{
    public class UserResponseContents
    {
        [JsonProperty(@"status")]
        public Status Status { get; set; }

        [JsonProperty(@"data")]
        public UserResponseContentsData Data { get; set; }
    }
}
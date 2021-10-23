using DisneyDown.Common.API.Structures.ApiDevice;
using Newtonsoft.Json;

namespace DisneyDown.Common.API.Structures.RequestPayloads
{
    public class DeviceGrantRequestPayload
    {
        [JsonProperty("applicationRuntime")]
        public string Runtime { get; set; } = @"";

        [JsonProperty("deviceFamily")]
        public string DeviceFamily { get; set; } = @"";

        [JsonProperty("deviceProfile")]
        public string DeviceProfile { get; set; } = @"";

        [JsonProperty("attributes", NullValueHandling = NullValueHandling.Include)]
        public ApiDeviceAttributes Attributes { get; set; } = new ApiDeviceAttributes();
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDeviceAttributes
    {
        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; } = @"";

        [JsonProperty("model")]
        public string Model { get; set; } = @"";

        [JsonProperty("operatingSystem")]
        public string OperatingSystem { get; set; } = @"";

        [JsonProperty("operatingSystemVersion")]
        public string OperatingSystemVersion { get; set; } = @"";

        [JsonProperty("osDeviceIds")]
        public List<ApiDeviceId> OsDeviceIds { get; set; } = new List<ApiDeviceId>();
    }
}
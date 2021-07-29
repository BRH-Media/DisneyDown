using Newtonsoft.Json;

namespace DisneyDown.Common.KeySystem
{
    public class ServiceInfo
    {
        public string ServerAddress { get; set; } = @"";
        public int ServerPort { get; set; } = 80;
        public bool Https { get; set; } = false;
        public bool ServiceEnabled { get; set; } = false;

        [JsonIgnore]
        public string BaseService => $"{(Https ? "https" : "http")}://{ServerAddress}:{ServerPort}";
    }
}
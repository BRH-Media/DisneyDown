using Newtonsoft.Json;

namespace DisneyDown.Common.API.Schemas.ServicesSchema
{
    public class Logger
    {
        [JsonProperty("logLevel")]
        public string LogLevel { get; set; }
    }
}
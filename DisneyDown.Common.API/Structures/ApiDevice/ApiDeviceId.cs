using Newtonsoft.Json;
using System;

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDeviceId
    {
        [JsonProperty("identifier")]
        public string Identifier { get; set; } = Guid.NewGuid().ToString();

        [JsonProperty("type")]
        public string Type { get; set; } = @"unknown";
    }
}
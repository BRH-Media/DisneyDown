using Newtonsoft.Json;

// ReSharper disable InconsistentNaming

namespace WVCore.Schemas
{
    public class WVErrorListing
    {
        [JsonProperty("errors", NullValueHandling = NullValueHandling.Ignore)]
        public Error[] Errors { get; set; }

        public static WVErrorListing FromJson(string json) => JsonConvert.DeserializeObject<WVErrorListing>(json, WVGenericSchemaConverter.Settings)!;
    }

    public class Error
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string Code { get; set; } = @"";

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = @"";
    }
}
﻿using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Structures.RequestPayloads;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDevice
    {
        [JsonProperty("BAMSDK_PLATFORM")]
        public string BamSdkPlatform { get; set; } = @"";

        [JsonProperty("BAMSDK_VERSION")]
        public string BamSdkVersion { get; set; } = @"";

        [JsonProperty("API_KEY")]
        public string ApiKey { get; set; } = @"";

        [JsonProperty("RUNTIME")]
        public string Runtime { get; set; } = @"";

        [JsonProperty("DEVICE_FAMILY")]
        public string DeviceFamily { get; set; } = @"";

        [JsonProperty("DEVICE_PROFILE")]
        public string DeviceProfile { get; set; } = @"";

        [JsonProperty("ATTRIBUTES", NullValueHandling = NullValueHandling.Ignore)]
        public ApiDeviceAttributes Attributes { get; set; } = new ApiDeviceAttributes();

        [JsonIgnore]
        public DeviceGrantRequestPayload DeviceRequestPayload => new DeviceGrantRequestPayload()
        {
            Runtime = Runtime,
            DeviceFamily = DeviceFamily,
            DeviceProfile = DeviceProfile,
            Attributes = Attributes
        };

        public TokenGrantResponse RequestDeviceToken()
        {
            try
            {
                //JSON request data
                var jsonPayload = JsonConvert.SerializeObject(DeviceRequestPayload, Converter.Settings);

                //setup client
                var client = new RestClient(Objects.Services.Services.Device.Client.BaseUrl);

                //setup request
                var request = new RestRequest(Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Href)
                {
                    Method = Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Method,
                    Resource = Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Href.GetEndpoint()
                };

                //apply body data
                request.AddParameter(@"application/json", jsonPayload, ParameterType.RequestBody);

                //generic headers
                request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", BamSdkPlatform));
                request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", BamSdkVersion));

                //request-specific headers
                request.AddHeader(@"Authorization", Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Headers.Authorization.Replace(@"{apiKey}", ApiKey));
                request.AddHeader(@"Accept",
                    Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Headers.Accept);
                request.AddHeader(@"Content-Type",
                    Objects.Services.Services.Device.Client.Endpoints.CreateDeviceGrant.Headers.ContentType);

                //execute and get response
                var response = client.Execute(request);

                //serialise the response
                var responseEncoded =
                    JsonConvert.DeserializeObject<TokenGrantResponse>(response.Content, Converter.Settings);

                //return the result
                return responseEncoded;
            }
            catch (Exception ex)
            {
                //error handling
                ConsoleWriters.ConsoleWriteError($"Device token request error: {ex.Message}");
            }

            //default
            return null;
        }
    }
}
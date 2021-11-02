using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;

namespace DisneyDown.Common.API
{
    public static class MediaManager
    {
        public static DmcVideoBundleSchema RequestVideoBundle(this ApiDevice deviceContext, string videoBundleId, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(videoBundleId))
                {
                    //setup client
                    var client = new RestClient(Objects.Services.Services.Content.Client.BaseUrl);

                    //replace out unneeded data
                    var placeholders = new List<(string placeholder, string value)>
                    {
                        ("apiVersion", "5.1"),
                        ("region", deviceContext.BamSdkCountry),
                        ("kidsModeEnabled", "false"),
                        ("impliedMaturityRating", "9999"),
                        ("appLanguage", "en-GB"),
                        ("encodedFamilyId", videoBundleId)
                    };
                    var endpoint =
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Href.GetEndpoint();
                    foreach (var (placeholder, value) in placeholders)
                        endpoint = endpoint.Replace($"{{{placeholder}}}", value);

                    ConsoleWriters.ConsoleWriteDebug(endpoint);

                    //setup request
                    var request = new RestRequest(Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Href)
                    {
                        Method = Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Method,
                        Resource = endpoint
                    };

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //request-specific headers
                    request.AddHeader(@"Authorization", Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Headers.Authorization.Replace(@"{accessToken}", token));
                    request.AddHeader(@"Accept",
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Headers.Accept);
                    request.AddHeader(@"Content-Type",
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideoBundle.Headers.ContentType);

                    //execute and get response
                    var response = client.Execute(request);
                    ConsoleWriters.ConsoleWriteDebug(response.Content);

                    //serialise the response
                    var responseEncoded =
                        JsonConvert.DeserializeObject<DmcVideoBundleSchema>(response.Content, ApiJsonConverter.Settings);

                    //return the result
                    return responseEncoded;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ video bundle: {ex}");
            }

            //default
            return null;
        }
    }
}
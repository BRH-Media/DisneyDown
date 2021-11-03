using DisneyDown.Common.API.Enums;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer;
using DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using DIsneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using PlaybackUrl = DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer.PlaybackUrl;

namespace DisneyDown.Common.API
{
    public static class MediaManager
    {
        public static string RequestManifestFromUrl(this ApiDevice deviceContext, string disneyUrl)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(disneyUrl))
                {
                    //make sure this isn't a series
                    if (!disneyUrl.ToLower().Contains(@"/series/"))
                    {
                        //alert user
                        ConsoleWriters.ConsoleWriteInfo(@"Requesting device grant token");

                        //request a device grant
                        var deviceGrantToken = Objects.Configuration.DeviceContext.RequestDeviceGrant();

                        //validation
                        if (!string.IsNullOrWhiteSpace(deviceGrantToken?.Assertion))
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteSuccess(@"Device grant successfully retrieved");
                            ConsoleWriters.ConsoleWriteInfo(@"Requesting device OAuth token");

                            //exchange the token
                            var deviceAuth =
                                Objects.Configuration.DeviceContext.PerformTokenExchange(deviceGrantToken.Assertion,
                                    ExchangeType.DEVICE);

                            //validation
                            if (!string.IsNullOrWhiteSpace(deviceAuth?.AccessToken))
                            {
                                //alert user
                                ConsoleWriters.ConsoleWriteSuccess(@"Device is authenticated");
                                ConsoleWriters.Break();

                                //credentials
                                var creds = Objects.Configuration.Credentials;

                                //validation
                                if (!string.IsNullOrWhiteSpace(creds.Email) &&
                                    !string.IsNullOrWhiteSpace(creds.Password))
                                {
                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo(
                                        $"Attempting to login with account \"{creds.Email}\"");

                                    //request login
                                    var loginToken = Objects.Configuration.DeviceContext.Login(deviceAuth.AccessToken);

                                    //validation
                                    if (!string.IsNullOrWhiteSpace(loginToken?.IdToken))
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteSuccess(@"Successfully logged into Disney+");
                                        ConsoleWriters.ConsoleWriteInfo(@"Requesting an account grant");

                                        //request account grant token
                                        var accountGrantToken =
                                            Objects.Configuration.DeviceContext.RequestAccountGrant(loginToken,
                                                deviceAuth.AccessToken);

                                        //validation
                                        if (!string.IsNullOrWhiteSpace(accountGrantToken?.Assertion))
                                        {
                                            //alert user
                                            ConsoleWriters.ConsoleWriteSuccess(@"Successfully retrieved an account grant token");
                                            ConsoleWriters.ConsoleWriteInfo(@"Requesting an account OAuth token");

                                            //request account OAuth token
                                            var accountToken =
                                                Objects.Configuration.DeviceContext.PerformTokenExchange(
                                                    accountGrantToken.Assertion, ExchangeType.ACCOUNT);

                                            //validation
                                            if (!string.IsNullOrWhiteSpace(accountToken?.AccessToken))
                                            {
                                            }
                                        }
                                        else
                                        {
                                            //alert user
                                            ConsoleWriters.ConsoleWriteError(@"Account grant request failed; please ensure that your credentials are correct");
                                        }
                                    }
                                    else
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteError(@"Login failed; please verify that your credentials are correct");
                                    }
                                }
                                else
                                {
                                    //alert user
                                    ConsoleWriters.ConsoleWriteError(@"Account information is invalid; please specify valid credentials in the configuration file");
                                }
                            }
                            else
                            {
                                //alert user
                                ConsoleWriters.ConsoleWriteError(@"Device OAuth token was invalid; manifest retrieval failed");
                            }
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"Device grant token was invalid; manifest retrieval failed");
                        }
                    }
                    else
                    {
                        //alert user
                        ConsoleWriters.ConsoleWriteError(@"The URL that was provided belongs to a TV series; multiple manifests would result from parsing and as such, a specific video URL must be provided.");
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ manifest: {ex.Message}");
            }

            //default
            return @"";
        }

        public static PlaybackScenarioSchema RequestPlaybackInformation(this ApiDevice deviceContext, PlaybackUrl playbackUrl, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(playbackUrl?.Href))
                {
                    //setup client
                    var client = new RestClient(Objects.Services.Services.Media.Client.BaseUrl);

                    //replace out unneeded data
                    var placeholders = new List<(string placeholder, string value)>
                    {
                        ("apiVersion", "5.1"),
                        ("region", deviceContext.BamSdkCountry),
                        ("kidsModeEnabled", "false"),
                        ("impliedMaturityRating", "9999"),
                        ("appLanguage", "en-GB"),
                        ("scenario", Objects.Services.Services.Media.Extras.PlaybackScenarioDefault)
                    };
                    var endpoint = playbackUrl.Href.GetEndpoint();
                    foreach (var (placeholder, value) in placeholders)
                        endpoint = endpoint.Replace($"{{{placeholder}}}", value);

                    //setup request
                    var request = new RestRequest(Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Href)
                    {
                        Method = Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Method,
                        Resource = endpoint
                    };

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //request-specific headers
                    request.AddHeader(@"Authorization", Objects.Services.Services.Media.Client.Endpoints.Key.Headers.Authorization.Replace(@"{accessToken}", token));
                    request.AddHeader(@"Accept",
                        Objects.Services.Services.Media.Client.Endpoints.Key.Headers.Accept);

                    //execute and get response
                    var response = client.Execute(request);

                    //serialise the response
                    var responseEncoded =
                        JsonConvert.DeserializeObject<PlaybackScenarioSchema>(response.Content, ApiJsonConverter.Settings);

                    //return the result
                    return responseEncoded;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ playback information: {ex.Message}");
            }

            //default
            return null;
        }

        public static DmcVideoSchema RequestVideo(this ApiDevice deviceContext, string contentId, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token) && !string.IsNullOrWhiteSpace(contentId))
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
                        ("contentId", contentId)
                    };
                    var endpoint =
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Href.GetEndpoint();
                    foreach (var (placeholder, value) in placeholders)
                        endpoint = endpoint.Replace($"{{{placeholder}}}", value);

                    //setup request
                    var request = new RestRequest(Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Href)
                    {
                        Method = Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Method,
                        Resource = endpoint
                    };

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //request-specific headers
                    request.AddHeader(@"Authorization", Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Headers.Authorization.Replace(@"{accessToken}", token));
                    request.AddHeader(@"Accept",
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Headers.Accept);
                    request.AddHeader(@"Content-Type",
                        Objects.Services.Services.Content.Client.Endpoints.GetDmcVideo.Headers.ContentType);

                    //execute and get response
                    var response = client.Execute(request);

                    //serialise the response
                    var responseEncoded =
                        JsonConvert.DeserializeObject<DmcVideoSchema>(response.Content, ApiJsonConverter.Settings);

                    //return the result
                    return responseEncoded;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ video: {ex.Message}");
            }

            //default
            return null;
        }

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
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ video bundle: {ex.Message}");
            }

            //default
            return null;
        }
    }
}
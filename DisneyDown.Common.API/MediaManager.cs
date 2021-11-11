using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer;
using DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoSchemaContainer;
using DisneyDown.Common.API.Schemas.MediaSchemas.PlaybackScenarioSchemaContainer;
using DisneyDown.Common.API.Structures;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using PlaybackUrl = DisneyDown.Common.API.Schemas.MediaSchemas.DmcVideoBundleSchemaContainer.PlaybackUrl;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.API
{
    public static class MediaManager
    {
        public static MediaInformation RequestManifestFromUrl(this ApiDevice deviceContext, string url)
        {
            //the information to return
            var mediaInfo = new MediaInformation();

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(url))
                {
                    //validation
                    if (!url.Contains(@"/series/"))
                    {
                        //request authentication
                        var authPackage = deviceContext.RequestAuthenticationPackage();

                        //validation
                        if (authPackage.Account.IsValid)
                        {
                            //parse out the ID component
                            var contentId = url.GetFileNameFromUrl();

                            //validation
                            if (!string.IsNullOrWhiteSpace(contentId))
                            {
                                //figure out what to do
                                if (contentId.Length > 16)
                                {
                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo($@"Requesting video object for '{contentId}'");

                                    //must be a video
                                    var video = deviceContext.RequestVideo(contentId,
                                        authPackage.Account.OAuthToken.Token);

                                    //validation
                                    if (video?.Data?.DmcVideo?.Video?.MediaMetadata?.PlaybackUrls?.Length > 0)
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteInfo(@"Requesting playback information");

                                        //playback URL
                                        var playbackUrl = video.Data?.DmcVideo?.Video?.MediaMetadata?.PlaybackUrls[0];

                                        //validation
                                        if (!string.IsNullOrWhiteSpace(playbackUrl?.Href))
                                        {
                                            //retrieve playback information
                                            var playback = deviceContext.RequestPlaybackInformation(playbackUrl,
                                                authPackage.Account.OAuthToken.Token);

                                            //validation
                                            if (playback != null)
                                            {
                                                //manifest URL
                                                var manifestUrl = playback.Stream?.Complete.ToString();

                                                //alert user
                                                ConsoleWriters.ConsoleWriteSuccess($@"Successfully retrieved manifest URL: {manifestUrl}");

                                                //apply media properties
                                                mediaInfo.MediaTitle = video.Data?.DmcVideo?.Video?.Text?.Title?.Full?.Program?.Default?.Content;
                                                mediaInfo.MediaDescription = video.Data?.DmcVideo?.Video?.Text?.Description?.Full?.Program?.Default?.Content;
                                                mediaInfo.MediaReleaseYear = video.Data?.DmcVideo?.Video
                                                    ?.Releases.Length > 0
                                                    ? video.Data?.DmcVideo?.Video?.Releases[0].ReleaseYear.ToString()
                                                    : @"";
                                                mediaInfo.ManifestUrl = manifestUrl;
                                            }
                                            else
                                            {
                                                //alert user
                                                ConsoleWriters.ConsoleWriteError(@"Returned playback information object was null; manifest retrieval failed");
                                            }
                                        }
                                        else
                                        {
                                            //alert user
                                            ConsoleWriters.ConsoleWriteError(@"Playback information endpoint was null; manifest retrieval failed");
                                        }
                                    }
                                    else
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteError(@"Returned video object was null; manifest retrieval failed");
                                    }
                                }
                                else
                                {
                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo($@"Requesting video bundle for '{contentId}'");

                                    //must be a bundle
                                    var bundle = deviceContext.RequestVideoBundle(contentId,
                                        authPackage.Account.OAuthToken.Token);

                                    //validation
                                    if (bundle?.Data?.DmcVideoBundle?.Video?.MediaMetadata?.PlaybackUrls?.Length > 0)
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteInfo(@"Requesting playback information");

                                        //playback URL
                                        var playbackUrl = bundle.Data?.DmcVideoBundle?.Video?.MediaMetadata
                                            ?.PlaybackUrls[0];

                                        //validation
                                        if (!string.IsNullOrWhiteSpace(playbackUrl?.Href))
                                        {
                                            //retrieve playback information
                                            var playback = deviceContext.RequestPlaybackInformation(playbackUrl,
                                                authPackage.Account.OAuthToken.Token);

                                            //validation
                                            if (playback != null)
                                            {
                                                //manifest URL
                                                var manifestUrl = playback.Stream?.Complete.ToString();

                                                //alert user
                                                ConsoleWriters.ConsoleWriteSuccess($@"Successfully retrieved manifest URL: {manifestUrl}");

                                                //apply media properties
                                                mediaInfo.MediaTitle = bundle.Data?.DmcVideoBundle?.Video?.Text?.Title
                                                    ?.Full?.Program?.Default?.Content;
                                                mediaInfo.MediaDescription = bundle.Data?.DmcVideoBundle?.Video?.Text?.Description
                                                    ?.Full?.Program?.Default?.Content;
                                                mediaInfo.MediaReleaseYear = bundle.Data?.DmcVideoBundle?.Video
                                                    ?.Releases.Length > 0
                                                        ? bundle.Data?.DmcVideoBundle?.Video?.Releases[0].ReleaseYear.ToString()
                                                        : @"";
                                                mediaInfo.ManifestUrl = manifestUrl;
                                            }
                                            else
                                            {
                                                //alert user
                                                ConsoleWriters.ConsoleWriteError(@"Returned playback information object was null; manifest retrieval failed");
                                            }
                                        }
                                        else
                                        {
                                            //alert user
                                            ConsoleWriters.ConsoleWriteError(@"Playback information endpoint was null; manifest retrieval failed");
                                        }
                                    }
                                    else
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteError(@"Returned video bundle object was null; manifest retrieval failed");
                                    }
                                }
                            }
                            else
                            {
                                //alert user
                                ConsoleWriters.ConsoleWriteError(@"Couldn't parse the content ID/bundle ID from the provided URL; manifest retrieval failed");
                            }
                        }
                        else
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteError(
                                @"Authentication provider reported an invalid set of tokens; manifest retrieval failed");
                        }
                    }
                    else
                    {
                        //alert user
                        ConsoleWriters.ConsoleWriteError("Series URLs are not supported as they yield multiple manifests;" +
                                                         " please specify an individual video URL if you wish to download an episode");
                    }
                }
                else
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteError(@"An invalid Disney+ URL was specified; manifest retrieval failed");
                }
            }
            catch (Exception ex)
            {
                //alert user
                ConsoleWriters.ConsoleWriteError($@"Manifest retrieval error: {ex.Message}");
            }

            //default
            return mediaInfo;
        }

        public static PlaybackScenarioSchema RequestPlaybackInformation(this ApiDevice deviceContext,
            Schemas.MediaSchemas.DmcVideoSchemaContainer.PlaybackUrl playbackUrl, string token)
            => deviceContext.RequestPlaybackInformation(new PlaybackUrl
            { Href = playbackUrl.Href, Rel = playbackUrl.Rel, Templated = playbackUrl.Templated }, token);

        public static PlaybackScenarioSchema RequestPlaybackInformation(this ApiDevice deviceContext,
            Schemas.MediaSchemas.DmcSeriesBundleSchemaContainer.PlaybackUrl playbackUrl, string token)
            => deviceContext.RequestPlaybackInformation(new PlaybackUrl
            { Href = playbackUrl.Href, Rel = playbackUrl.Rel, Templated = playbackUrl.Templated }, token);

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
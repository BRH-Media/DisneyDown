using DisneyDown.Common.API.Enums;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas.DrmSchemas.NagraServiceCertificateSchemaContainer;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using RestSharp;
using System;
using System.Net;
using System.Text;

namespace DisneyDown.Common.API
{
    public static class DrmManager
    {
        public static NagraServiceCertificateSchema RetrieveNagraCertificate(this ApiDevice deviceContext)
        {
            try
            {
                //get certificate
                var nagraCertData = RetrieveServiceCertificate(deviceContext, DrmType.NAGRA);

                //validation
                if (nagraCertData != null)
                {
                    //convert to a string
                    var nagraCert = Encoding.UTF8.GetString(nagraCertData);

                    //validation
                    if (!string.IsNullOrWhiteSpace(nagraCert))
                    {
                        //return the deserialised result
                        return NagraServiceCertificateSchema.FromJson(nagraCert);
                    }
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Error retrieving Nagra Service Certificate: {ex.Message}");
            }

            //default
            return null;
        }

        public static byte[] RetrieveFairPlayCertificate(this ApiDevice deviceContext)
            => RetrieveServiceCertificate(deviceContext, DrmType.FAIRPLAY);

        public static byte[] RetrieveWidevineCertificate(this ApiDevice deviceContext)
            => RetrieveServiceCertificate(deviceContext, DrmType.WIDEVINE);

        public static byte[] RetrieveServiceCertificate(this ApiDevice deviceContext, DrmType type)
        {
            try
            {
                //validation
                if (Objects.Services != null)
                {
                    //setup client
                    var client = new RestClient(Objects.Services.Services.Drm.Client.BaseUrl);

                    //setup request
                    var request = new RestRequest();

                    //drm-specific
                    switch (type)
                    {
                        //Nagra PRM
                        case DrmType.NAGRA:

                            //alert user
                            ConsoleWriters.ConsoleWriteInfo(@"Requesting Nagra PRM Certificate");

                            //Nagra configuration (from services JSON)
                            var nagra = Objects.Services.Services.Drm.Client.Endpoints.NagraCertificate;

                            //apply Nagra-specific request information
                            request.Resource = nagra.Href;
                            request.Method = nagra.Method;

                            break;

                        //Google Widevine
                        case DrmType.WIDEVINE:

                            //alert user
                            ConsoleWriters.ConsoleWriteInfo(@"Requesting Widevine Certificate");

                            //Widevine configuration (from services JSON)
                            var widevine = Objects.Services.Services.Drm.Client.Endpoints.WidevineCertificate;

                            //apply Widevine-specific request information
                            request.Resource = widevine.Href;
                            request.Method = widevine.Method;

                            break;

                        //Apple FairPlay
                        case DrmType.FAIRPLAY:

                            //alert user
                            ConsoleWriters.ConsoleWriteInfo(@"Requesting FairPlay Certificate");

                            //FairPlay configuration (from services JSON)
                            var fairplay = Objects.Services.Services.Drm.Client.Endpoints.FairPlayCertificate;

                            //apply FairPlay-specific request information
                            request.Method = fairplay.Method;
                            request.Resource = fairplay.Href;

                            break;

                        //Microsoft PlayReady
                        case DrmType.PLAYREADY:

                            //alert user
                            ConsoleWriters.ConsoleWriteError(@"PlayReady certificates are not in use by Disney+");

                            //exit function
                            return null;
                    }

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //execute and get response
                    var response = client.Execute(request);

                    //validation
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //alert user
                        ConsoleWriters.ConsoleWriteSuccess($"Received valid DRM service certificate ({type})");

                        //return response bytes
                        return response.RawBytes;
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM service certificate ({type}): License response did not return HTTP/S OK");
                    }
                }
                else
                {
                    //report error
                    ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM service certificate ({type}): System is not provisioned");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM service certificate ({type}): {ex.Message}");
            }

            //default
            return null;
        }

        public static byte[] PerformLicenseExchange(this ApiDevice deviceContext, byte[] licenseRequest, DrmType type)
        {
            try
            {
                //validation
                if (Objects.Services != null)
                {
                    //authenticate
                    var auth = AuthManager.RequestAuthenticationPackage(deviceContext);

                    //validation
                    if (auth != null)
                    {
                        //validation
                        if (auth.Account.OAuthToken.IsValid)
                        {
                            //authentication token
                            var token = auth.Account.OAuthToken.Token;

                            //setup client
                            var client = new RestClient(Objects.Services.Services.Drm.Client.BaseUrl);

                            //setup request
                            var request = new RestRequest();

                            //drm-specific
                            switch (type)
                            {
                                //Nagra PRM
                                case DrmType.NAGRA:

                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo(@"Requesting Nagra PRM License");

                                    //Nagra configuration (from services JSON)
                                    var nagra = Objects.Services.Services.Drm.Client.Endpoints.NagraLicense;

                                    //setup Nagra-specific headers
                                    request.AddHeader(@"Authorization", nagra.Headers.Authorization.Replace(@"{accessToken}", token));
                                    request.AddHeader(@"Accept", nagra.Headers.Accept);
                                    request.AddHeader(@"Content-Type", nagra.Headers.ContentType);

                                    //apply Nagra-specific request information
                                    request.Resource = nagra.Href;
                                    request.Method = nagra.Method;

                                    break;

                                //Google Widevine
                                case DrmType.WIDEVINE:

                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo(@"Requesting Widevine License");

                                    //Widevine configuration (from services JSON)
                                    var widevine = Objects.Services.Services.Drm.Client.Endpoints.WidevineLicense;

                                    //setup Widevine-specific headers
                                    request.AddHeader(@"Authorization", widevine.Headers.Authorization.Replace(@"{accessToken}", token));
                                    request.AddHeader(@"Content-Type", widevine.Headers.ContentType);

                                    //apply Widevine-specific request information
                                    request.Resource = widevine.Href;
                                    request.Method = widevine.Method;

                                    break;

                                //Apple FairPlay
                                case DrmType.FAIRPLAY:

                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo(@"Requesting FairPlay License");

                                    //FairPlay configuration (from services JSON)
                                    var fairplay = Objects.Services.Services.Drm.Client.Endpoints.FairPlayLicense;

                                    //setup FairPlay-specific headers
                                    request.AddHeader(@"Authorization", fairplay.Headers.Authorization.Replace(@"{accessToken}", token));
                                    request.AddHeader(@"Accept", fairplay.Headers.Accept);
                                    request.AddHeader(@"Content-Type", fairplay.Headers.ContentType);

                                    //apply FairPlay-specific request information
                                    request.Method = fairplay.Method;
                                    request.Resource = fairplay.Href;

                                    break;

                                //Microsoft PlayReady
                                case DrmType.PLAYREADY:

                                    //alert user
                                    ConsoleWriters.ConsoleWriteInfo(@"Requesting PlayReady License");

                                    //PlayReady configuration (from services JSON)
                                    var playready = Objects.Services.Services.Drm.Client.Endpoints.PlayReadyLicense;

                                    //setup PlayReady-specific headers
                                    request.AddHeader(@"Authorization", playready.Headers.Authorization.Replace(@"{accessToken}", token));
                                    request.AddHeader(@"Accept", playready.Headers.Accept);
                                    request.AddHeader(@"Content-Type", playready.Headers.ContentType);

                                    //apply PlayReady-specific request information
                                    request.Method = playready.Method;
                                    request.Resource = playready.Href;

                                    break;
                            }

                            //generic headers
                            request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                            request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                            //execute and get response
                            var response = client.Execute(request);

                            //validation
                            if (response.StatusCode == HttpStatusCode.OK)
                            {
                                //alert user
                                ConsoleWriters.ConsoleWriteSuccess(@"Received valid license response");

                                //return response bytes
                                return response.RawBytes;
                            }
                            else
                            {
                                //report error
                                ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM license ({type}): License response did not return HTTP/S OK");
                            }
                        }
                        else
                        {
                            //report error
                            ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM license ({type}): User is unauthorised");
                        }
                    }
                    else
                    {
                        //report error
                        ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM license ({type}): User is unauthorised");
                    }
                }
                else
                {
                    //report error
                    ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM license ({type}): System is not provisioned");
                }
            }
            catch (Exception ex)
            {
                //report error
                ConsoleWriters.ConsoleWriteError($"Error occurred while obtaining DRM license ({type}): {ex.Message}");
            }

            //default
            return null;
        }
    }
}
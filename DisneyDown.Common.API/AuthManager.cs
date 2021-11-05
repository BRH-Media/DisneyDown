using DisneyDown.Common.API.Enums;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas.AuthenticationSchemas;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using Microsoft.IdentityModel.JsonWebTokens;
using Newtonsoft.Json;
using RestSharp;
using System;
using Converter = DisneyDown.Common.Util.Converter;

// ReSharper disable RedundantIfElseBlock

namespace DisneyDown.Common.API
{
    public static class AuthManager
    {
        public static ApiDeviceTokenStorage RequestAuthenticationPackage(this ApiDevice deviceContext)
        {
            try
            {
                //alert user
                ConsoleWriters.ConsoleWriteDebug(@"Requesting device grant token");

                //request a device grant
                var deviceGrantToken = Objects.Configuration.DeviceContext.RequestDeviceGrant();

                //validation
                if (!string.IsNullOrWhiteSpace(deviceGrantToken?.Assertion))
                {
                    //alert user
                    ConsoleWriters.ConsoleWriteDebug(@"Device grant successfully retrieved");
                    ConsoleWriters.ConsoleWriteDebug(@"Requesting device OAuth token");

                    //exchange the token
                    var deviceAuth =
                        Objects.Configuration.DeviceContext.PerformTokenExchange(deviceGrantToken.Assertion,
                            ExchangeType.DEVICE);

                    //validation
                    if (!string.IsNullOrWhiteSpace(deviceAuth?.AccessToken))
                    {
                        //alert user
                        ConsoleWriters.ConsoleWriteDebug(@"Device is authenticated via a device grant");

                        //credentials
                        var creds = Objects.Configuration.Credentials;

                        //validation
                        if (!string.IsNullOrWhiteSpace(creds.Email) &&
                            !string.IsNullOrWhiteSpace(creds.Password))
                        {
                            //alert user
                            ConsoleWriters.ConsoleWriteDebug(
                                $"Attempting to login with account \"{creds.Email}\"");

                            //request login
                            var loginToken = Objects.Configuration.DeviceContext.Login(deviceAuth.AccessToken);

                            //validation
                            if (!string.IsNullOrWhiteSpace(loginToken?.IdToken))
                            {
                                //alert user
                                ConsoleWriters.ConsoleWriteDebug(@"Successfully logged into Disney+");
                                ConsoleWriters.ConsoleWriteDebug(@"Requesting an account grant");

                                //request account grant token
                                var accountGrantToken =
                                    Objects.Configuration.DeviceContext.RequestAccountGrant(loginToken,
                                        deviceAuth.AccessToken);

                                //validation
                                if (!string.IsNullOrWhiteSpace(accountGrantToken?.Assertion))
                                {
                                    //alert user
                                    ConsoleWriters.ConsoleWriteDebug(@"Successfully retrieved an account grant token");
                                    ConsoleWriters.ConsoleWriteDebug(@"Requesting an account OAuth token");

                                    //request account OAuth token
                                    var accountToken =
                                        Objects.Configuration.DeviceContext.PerformTokenExchange(
                                            accountGrantToken.Assertion, ExchangeType.ACCOUNT);

                                    //validation
                                    if (!string.IsNullOrWhiteSpace(accountToken?.AccessToken))
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteSuccess(@"Successfully authenticated device with a Disney+ account");

                                        //return the fully-constructed authentication object
                                        return new ApiDeviceTokenStorage
                                        {
                                            Device =
                                                {
                                                    GrantToken = deviceGrantToken.GetDisneyToken(),
                                                    OAuthToken = deviceAuth.GetDisneyToken()
                                                },
                                            Account =
                                                {
                                                    GrantToken = accountGrantToken.GetDisneyToken(),
                                                    OAuthToken = accountToken.GetDisneyToken()
                                                },
                                            Identity =
                                                {
                                                    GrantToken = loginToken.GetDisneyToken()
                                                },
                                            Refresh =
                                                {
                                                    OAuthToken =
                                                    {
                                                        Expiry = new JsonWebToken(accountToken.RefreshToken).ValidTo.ToLocalTime(),
                                                        Token = accountToken.RefreshToken,
                                                        Status = TokenStatus.GRANTED
                                                    }
                                                }
                                        };
                                    }
                                    else
                                    {
                                        //alert user
                                        ConsoleWriters.ConsoleWriteError(@"Account OAuth request failed; please ensure that your credentials are correct");
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
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to get Disney+ manifest: {ex.Message}");
            }

            //default
            return new ApiDeviceTokenStorage();
        }

        public static TokenGrantResponse RequestAccountGrant(this ApiDevice deviceContext, BAMIdentityResponse identity, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(identity.IdToken))
                {
                    //query string request data
                    var jsonStringPayload = JsonConvert.SerializeObject(identity);

                    //setup client
                    var client = new RestClient(Objects.Services.Services.Account.Client.BaseUrl);

                    //setup request
                    var request = new RestRequest(Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Href)
                    {
                        Method = Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Method,
                        Resource = Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Href.GetEndpoint()
                    };

                    //apply body data
                    request.AddParameter(@"application/json", jsonStringPayload, ParameterType.RequestBody);

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //request-specific headers
                    request.AddHeader(@"Authorization", Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Headers.Authorization.Replace(@"{accessToken}", token));
                    request.AddHeader(@"Accept",
                        Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Headers.Accept);
                    request.AddHeader(@"Content-Type",
                        Objects.Services.Services.Account.Client.Endpoints["createAccountGrant"].Headers.ContentType);

                    //execute and get response
                    var response = client.Execute(request);

                    //serialise the response
                    var responseEncoded =
                        JsonConvert.DeserializeObject<TokenGrantResponse>(response.Content, Converter.Settings);

                    //return the result
                    return responseEncoded;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to login to Disney+: {ex.Message}");
            }

            //default
            return null;
        }

        public static BAMIdentityResponse Login(this ApiDevice deviceContext, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token))
                {
                    //query string request data
                    var jsonStringPayload = JsonConvert.SerializeObject(Objects.Configuration.Credentials);

                    //setup client
                    var client = new RestClient(Objects.Services.Services.BamIdentity.Client.BaseUrl);

                    //setup request
                    var request = new RestRequest(Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Href)
                    {
                        Method = Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Method,
                        Resource = Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Href.GetEndpoint()
                    };

                    //apply body data
                    request.AddParameter(@"application/json", jsonStringPayload, ParameterType.RequestBody);

                    //generic headers
                    request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                    request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                    //request-specific headers
                    request.AddHeader(@"Authorization", Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Headers.Authorization.Replace(@"{accessToken}", token));
                    request.AddHeader(@"Accept",
                        Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Headers.Accept);
                    request.AddHeader(@"Content-Type",
                        Objects.Services.Services.BamIdentity.Client.Endpoints["identityLogin"].Headers.ContentType);

                    //execute and get response
                    var response = client.Execute(request);

                    //serialise the response
                    var responseEncoded =
                        JsonConvert.DeserializeObject<BAMIdentityResponse>(response.Content, Converter.Settings);

                    //return the result
                    return responseEncoded;
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while trying to login to Disney+: {ex.Message}");
            }

            //default
            return null;
        }
    }
}
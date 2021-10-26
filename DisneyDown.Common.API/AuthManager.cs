using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Structures;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;
using System;

namespace DisneyDown.Common.API
{
    public static class AuthManager
    {
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

        public static BAMIdentityResponse Login(this ApiDevice deviceContext, ApiConfigurationCredentials credentials, string token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token))
                {
                    //query string request data
                    var jsonStringPayload = JsonConvert.SerializeObject(credentials);

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
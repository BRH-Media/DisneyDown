using DisneyDown.Common.API.Enums;
using DisneyDown.Common.API.Globals;
using DisneyDown.Common.API.Schemas;
using DisneyDown.Common.API.Structures.ApiDevice;
using DisneyDown.Common.API.Structures.RequestPayloads;
using DisneyDown.Common.Util.Kit;
using Newtonsoft.Json;
using RestSharp;
using System;
using Converter = DisneyDown.Common.Util.Converter;

namespace DisneyDown.Common.API
{
    public static class TokenManager
    {
        public static TokenExchangeResponse PerformTokenExchange(this ApiDevice deviceContext, string token, ExchangeType tokenType)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token))
                {
                    //parse the token type into a grant type
                    var type = tokenType.GetDescription();

                    //validation
                    if (!string.IsNullOrWhiteSpace(type))
                    {
                        //payload
                        var requestData = new TokenExchangeRequestPayload();

                        //setup the payload
                        switch (tokenType)
                        {
                            case ExchangeType.ACCOUNT:
                            case ExchangeType.DEVICE:

                                //setup account/device token exchange properties
                                requestData.platform = deviceContext.DeviceProfile;
                                requestData.subject_token = token;
                                requestData.subject_token_type = tokenType.GetDescription();
                                requestData.grant_type = ExchangeType.EXCHANGE.GetDescription();
                                break;

                            case ExchangeType.REFRESH:

                                //setup refresh token grant properties
                                requestData.platform = deviceContext.DeviceProfile;
                                requestData.refresh_token = token;
                                requestData.grant_type = "refresh_token";
                                break;
                        }

                        //query string request data
                        var queryStringPayload = requestData.ConvertToQueryString();

                        //setup client
                        var client = new RestClient(Objects.Services.Services.Device.Client.BaseUrl);

                        //setup request
                        var request = new RestRequest(Objects.Services.Services.Token.Client.Endpoints.Exchange.Href)
                        {
                            Method = Objects.Services.Services.Token.Client.Endpoints.Exchange.Method,
                            Resource = Objects.Services.Services.Token.Client.Endpoints.Exchange.Href.GetEndpoint()
                        };

                        //apply body data
                        request.AddParameter(@"application/x-www-form-urlencoded", queryStringPayload, ParameterType.RequestBody);

                        //generic headers
                        request.AddHeader(@"X-BAMSDK-PLATFORM", Objects.Services.CommonHeaders.XBamsdkPlatform.Replace("{SDKPlatform}", deviceContext.BamSdkPlatform));
                        request.AddHeader(@"X-BAMSDK-VERSION", Objects.Services.CommonHeaders.XBamsdkVersion.Replace("{SDKVersion}", deviceContext.BamSdkVersion));

                        //request-specific headers
                        request.AddHeader(@"Authorization", Objects.Services.Services.Token.Client.Endpoints.Exchange.Headers.Authorization.Replace(@"{apiKey}", deviceContext.ApiKey));
                        request.AddHeader(@"Accept",
                            Objects.Services.Services.Token.Client.Endpoints.Exchange.Headers.Accept);
                        request.AddHeader(@"Content-Type",
                            Objects.Services.Services.Token.Client.Endpoints.Exchange.Headers.ContentType);

                        //execute and get response
                        var response = client.Execute(request);

                        //serialise the response
                        var responseEncoded =
                            JsonConvert.DeserializeObject<TokenExchangeResponse>(response.Content, Converter.Settings);

                        //return the result
                        return responseEncoded;
                    }
                }
            }
            catch (Exception ex)
            {
                //handle error
                ConsoleWriters.ConsoleWriteDebug($"Experienced an error while exchanging token: {ex.Message}");
            }

            //default
            return null;
        }
    }
}
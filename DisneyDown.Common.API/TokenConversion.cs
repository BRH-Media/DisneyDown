using DisneyDown.Common.API.Enums;
using DisneyDown.Common.API.Schemas.AuthenticationSchemas;
using DisneyDown.Common.Util.Kit;
using Microsoft.IdentityModel.JsonWebTokens;
using System;

namespace DisneyDown.Common.API
{
    public static class TokenConversion
    {
        public static DisneyToken GetDisneyToken(this TokenGrantResponse token)
        {
            //object to be returned
            var resultingToken = new DisneyToken
            {
                Status = TokenStatus.NOT_SET
            };

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.Assertion))
                {
                    //parse the token
                    var jwt = new JsonWebToken(token.Assertion);

                    //return resulting token object
                    resultingToken = new DisneyToken
                    {
                        Token = token.Assertion,
                        Status = TokenStatus.GRANTED,
                        Expiry = jwt.ValidTo
                    };
                }
            }
            catch (Exception ex)
            {
                //change token status
                resultingToken.Status = TokenStatus.ERRORED;
                resultingToken.LastError = ex;

                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return resultingToken;
        }

        public static DisneyToken GetDisneyToken(this TokenExchangeResponse token)
        {
            //object to be returned
            var resultingToken = new DisneyToken
            {
                Status = TokenStatus.NOT_SET
            };

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.AccessToken))
                {
                    //convert validity number to an expiry time
                    var expiry = DateTime.Now + TimeSpan.FromMilliseconds(token.ExpiresIn);

                    //return resulting token object
                    resultingToken = new DisneyToken
                    {
                        Token = token.AccessToken,
                        Status = TokenStatus.GRANTED,
                        Expiry = expiry
                    };
                }
            }
            catch (Exception ex)
            {
                //change token status
                resultingToken.Status = TokenStatus.ERRORED;
                resultingToken.LastError = ex;

                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return resultingToken;
        }

        public static DisneyToken GetDisneyToken(this BAMIdentityResponse token)
        {
            //object to be returned
            var resultingToken = new DisneyToken
            {
                Status = TokenStatus.NOT_SET
            };

            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.IdToken))
                {
                    //parse the token
                    var jwt = new JsonWebToken(token.IdToken);

                    //return resulting token object
                    resultingToken = new DisneyToken
                    {
                        Token = token.IdToken,
                        Status = TokenStatus.GRANTED,
                        Expiry = jwt.ValidTo.ToLocalTime()
                    };
                }
            }
            catch (Exception ex)
            {
                //change token status
                resultingToken.Status = TokenStatus.ERRORED;
                resultingToken.LastError = ex;

                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return resultingToken;
        }
    }
}
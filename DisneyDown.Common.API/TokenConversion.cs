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
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.Assertion))
                {
                    //parse the token
                    var jwt = new JsonWebToken(token.Assertion);

                    //return resulting token object
                    return new DisneyToken
                    {
                        Token = token.Assertion,
                        Status = TokenStatus.GRANTED,
                        Expiry = jwt.ValidTo
                    };
                }
            }
            catch (Exception ex)
            {
                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return new DisneyToken();
        }

        public static DisneyToken GetDisneyToken(this TokenExchangeResponse token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.AccessToken))
                {
                    //convert validity number to an expiry time
                    var expiry = DateTime.Now + TimeSpan.FromMilliseconds(token.ExpiresIn);

                    //return resulting token object
                    return new DisneyToken
                    {
                        Token = token.AccessToken,
                        Status = TokenStatus.GRANTED,
                        Expiry = expiry
                    };
                }
            }
            catch (Exception ex)
            {
                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return new DisneyToken();
        }

        public static DisneyToken GetDisneyToken(this BAMIdentityResponse token)
        {
            try
            {
                //validation
                if (!string.IsNullOrWhiteSpace(token?.IdToken))
                {
                    //parse the token
                    var jwt = new JsonWebToken(token.IdToken);

                    //return resulting token object
                    return new DisneyToken
                    {
                        Token = token.IdToken,
                        Status = TokenStatus.GRANTED,
                        Expiry = jwt.ValidTo.ToLocalTime()
                    };
                }
            }
            catch (Exception ex)
            {
                //error reporting
                ConsoleWriters.ConsoleWriteDebug($@"Token object conversion error: {ex.Message}");
            }

            //default
            return new DisneyToken();
        }
    }
}
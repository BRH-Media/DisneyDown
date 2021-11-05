using DisneyDown.Common.API.Enums;

// ReSharper disable ReplaceAutoPropertyWithComputedProperty

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDeviceTokenObject
    {
        /// <summary>
        /// Grants are temporary tokens and do not typically give access to high-level endpoints
        /// </summary>
        public DisneyToken GrantToken { get; set; } = new DisneyToken();

        /// <summary>
        /// OAuth tokens are issued via grant exchanges and provide access to high-level endpoints (i.e. login and media information)
        /// </summary>
        public DisneyToken OAuthToken { get; set; } = new DisneyToken();

        /// <summary>
        /// What tokens are being stored here? You can use any enum member but it's recommended to use one that includes an OAuth type specifier
        /// </summary>
        public ExchangeType Type { get; } = ExchangeType.UNKNOWN;

        /// <summary>
        /// This tests both stored tokens and returns true if one or both are expired
        /// </summary>
        public bool IsExpired => GrantToken.IsExpired || OAuthToken.IsExpired;

        /// <summary>
        /// This tests both stored tokens and returns true if the token is set and the token status
        /// is correct
        /// </summary>
        public bool IsValid => GrantToken.IsValid && OAuthToken.IsValid;

        public ApiDeviceTokenObject()
        {
            //blank initialiser
        }

        public ApiDeviceTokenObject(ExchangeType type)
        {
            //setup exchange value type
            Type = type;
        }
    }
}
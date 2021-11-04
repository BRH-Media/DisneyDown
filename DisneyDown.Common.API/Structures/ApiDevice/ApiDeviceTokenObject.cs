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

        public ExchangeType Type { get; } = ExchangeType.UNKNOWN;

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
using DisneyDown.Common.API.Enums;

namespace DisneyDown.Common.API.Structures.ApiDevice
{
    public class ApiDeviceTokenStorage
    {
        /// <summary>
        /// Device tokens are temporary tokens; they are used to initiate authentication and other "ground-level" processes
        /// </summary>
        public ApiDeviceTokenObject Device { get; } = new ApiDeviceTokenObject(ExchangeType.DEVICE);

        /// <summary>
        /// Account tokens store authentication tokens; these are so-called "penthouse" tokens - they allow access to more or less the entire system
        /// </summary>

        public ApiDeviceTokenObject Account { get; } = new ApiDeviceTokenObject(ExchangeType.ACCOUNT);
        /// <summary>
        /// Identity tokens are a special type of token grant; they are intended to be used for an account grant and not really for anything else. Logging in
        /// with a username and password will yield one of these "BAMIdentity" tokens and they're invalidated when an account grant is requested.
        /// </summary>

        public ApiDeviceTokenObject Identity { get; } = new ApiDeviceTokenObject(ExchangeType.IDENTITY);

        /// <summary>
        /// Refresh tokens allow a renewed token to be requested without following the entire authentication procedure again.
        /// </summary>
        public ApiDeviceTokenObject Refresh { get; } = new ApiDeviceTokenObject(ExchangeType.REFRESH);

        /// <summary>
        /// This tests every stored token and returns true if one or more are expired
        /// </summary>

        public bool IsExpired => Device.IsExpired || Account.IsExpired || Identity.IsExpired || Refresh.IsExpired;

        /// <summary>
        /// This tests every stored token and returns true if all tokens are set and their statuses
        /// are correct
        /// </summary>
        public bool IsValid => Device.IsValid && Account.IsValid && Identity.IsValid && Refresh.IsValid;
    }
}
using DisneyDown.Common.API.Enums;
using System;

namespace DisneyDown.Common.API
{
    public class DisneyToken
    {
        /// <summary>
        /// The current JSON Web Token status
        /// </summary>
        public TokenStatus Status { get; set; } = TokenStatus.NOT_SET;

        /// <summary>
        /// Stores the encoded JSON Web Token
        /// </summary>
        public string Token { get; set; } = @"";

        /// <summary>
        /// Specifies when the JSON Web Token is set to expire (current time if none specified)
        /// </summary>
        public DateTime Expiry { get; set; } = DateTime.Now;

        /// <summary>
        /// Compares the expiry time to the current time and returns true if the expiry time is exceeded
        /// </summary>
        public bool IsExpired => DateTime.Now >= Expiry;

        /// <summary>
        /// Checks to see if the JSON Web Token is set and that the current status is correct and valid
        /// </summary>
        public bool IsValid => !string.IsNullOrWhiteSpace(Token)
                               && Status != TokenStatus.ERRORED
                               && Status != TokenStatus.NOT_SET;

        /// <summary>
        /// If the token conversion process encountered an error, the exception object will be stored here
        /// </summary>
        public Exception LastError { get; set; } = null;
    }
}
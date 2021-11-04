using DisneyDown.Common.API.Enums;
using System;

namespace DisneyDown.Common.API
{
    public class DisneyToken
    {
        public TokenStatus Status { get; set; } = TokenStatus.NOT_SET;
        public string Token { get; set; } = @"";
        public DateTime Expiry { get; set; } = DateTime.Now;
    }
}
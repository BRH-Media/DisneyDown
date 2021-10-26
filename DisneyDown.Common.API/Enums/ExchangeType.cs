// ReSharper disable UnusedMember.Global
// ReSharper disable ArrangeTrailingCommaInMultilineLists
// ReSharper disable InconsistentNaming

using System.ComponentModel;

namespace DisneyDown.Common.API.Enums
{
    public enum ExchangeType
    {
        [Description("urn:ietf:params:oauth:token-type:jwt")]
        TEMPORARY,

        [Description("urn:bamtech:params:oauth:token-type:device")]
        DEVICE,

        [Description("urn:bamtech:params:oauth:token-type:account")]
        ACCOUNT,

        [Description("urn:ietf:params:oauth:grant-type:token-exchange")]
        EXCHANGE,

        [Description("refresh")]
        REFRESH,

        IDENTITY,
        UNKNOWN
    }
}
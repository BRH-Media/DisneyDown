using System.Collections.Generic;

namespace DisneyDown.Console.Dash.Common.Protection
{
    public static class ProtectionSchemes
    {
        public static Dictionary<string, string> Definitions { get; } = new Dictionary<string, string>
        {
            {
                ProtectionUUID.Widevine,
                @""
            },
            {
                ProtectionUUID.PlayReady,
                @""
            }
        };
    }
}
using System.Collections.Generic;

namespace DisneyDown.Common.Interpreters.HLSParser
{
    public static class BandwidthDefinitions
    {
        public static Dictionary<int, string> Definitions { get; } = new Dictionary<int, string>()
        {
            {
                6,
                @"4250k"
            },
            {
                5,
                @"3000k"
            },
            {
                4,
                @"2400k"
            },
            {
                3,
                @"1800k"
            },
            {
                2,
                @"1200k"
            },
            {
                1,
                @"800k"
            },
            {
                0,
                @"400k"
            }
        };
    }
}
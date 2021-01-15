using System.Collections.Generic;

namespace DisneyDown.Common.Globals
{
    /// <summary>
    /// Stores bandwidth rating information used for manifest quality sorting
    /// </summary>
    public static class BandwidthDefinitions
    {
        public static Dictionary<int, string> VideoDefinitions { get; } = new Dictionary<int, string>
        {
            {
                10,
                @"8500k"
            },
            {
                9,
                @"7000k"
            },
            {
                8,
                @"5500k"
            },
            {
                7,
                @"4250k"
            },
            {
                6,
                @"3000k"
            },
            {
                5,
                @"2400k"
            },
            {
                4,
                @"1800k"
            },
            {
                3,
                @"1200k"
            },
            {
                2,
                @"800k"
            },
            {
                1,
                @"480k"
            },
            {
                0,
                @"400k"
            }
        };

        public static Dictionary<int, string> AudioDefinitions { get; } = new Dictionary<int, string>
        {
            {
                8,
                @"1000k"
            },
            {
                7,
                @"640k"
            },
            {
                6,
                @"512k"
            },
            {
                5,
                @"480k"
            },
            {
                4,
                @"400k"
            },
            {
                3,
                @"300k"
            },
            {
                2,
                @"256k"
            },
            {
                1,
                @"128k"
            },
            {
                0,
                @"64k"
            }
        };
    }
}
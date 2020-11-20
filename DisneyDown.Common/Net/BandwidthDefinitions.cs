using System.Collections.Generic;

namespace DisneyDown.Common.Net
{
    /// <summary>
    /// Stores bandwidth rating information used for manifest quality sorting
    /// </summary>
    public static class BandwidthDefinitions
    {
        public static Dictionary<int, string> VideoDefinitions { get; } = new Dictionary<int, string>
        {
            {
                7,
                @"4250"
            },
            {
                6,
                @"3000"
            },
            {
                5,
                @"2400"
            },
            {
                4,
                @"1800"
            },
            {
                3,
                @"1200"
            },
            {
                2,
                @"800"
            },
            {
                1,
                @"480"
            },
            {
                0,
                @"400"
            }
        };

        public static Dictionary<int, string> AudioDefinitions { get; } = new Dictionary<int, string>
        {
            {
                7,
                @"640"
            },
            {
                6,
                @"512"
            },
            {
                5,
                @"480"
            },
            {
                4,
                @"400"
            },
            {
                3,
                @"300"
            },
            {
                2,
                @"256"
            },
            {
                1,
                @"128"
            },
            {
                0,
                @"64"
            }
        };
    }
}
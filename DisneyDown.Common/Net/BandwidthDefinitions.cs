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
                6,
                @"4250"
            },
            {
                5,
                @"3000"
            },
            {
                4,
                @"2400"
            },
            {
                3,
                @"1800"
            },
            {
                2,
                @"1200"
            },
            {
                1,
                @"800"
            },
            {
                0,
                @"400"
            }
        };

        public static Dictionary<int, string> AudioDefinitions { get; } = new Dictionary<int, string>
        {
            {
                6,
                @"640"
            },
            {
                5,
                @"512"
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
                @"128K"
            },
            {
                0,
                @"64"
            }
        };
    }
}
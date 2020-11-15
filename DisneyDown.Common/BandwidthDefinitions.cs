using System.Collections.Generic;

namespace DisneyDown.Common
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

        public static Dictionary<int, string> AudioDefinitions { get; } = new Dictionary<int, string>
        {
            {
                6,
                @"640k"
            },
            {
                5,
                @"512k"
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
                @"128K"
            },
            {
                0,
                @"64k"
            }
        };
    }
}
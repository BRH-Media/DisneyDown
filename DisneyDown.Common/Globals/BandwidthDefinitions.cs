using DisneyDown.Common.Parsers;
using System.Collections.Generic;

namespace DisneyDown.Common.Globals
{
    /// <summary>
    /// Stores bandwidth rating information used for manifest quality sorting
    /// </summary>
    public static class BandwidthDefinitions
    {
        public static Dictionary<int, QualityRating> VideoDefinitions { get; } = new Dictionary<int, QualityRating>
        {
            {
                10,
                new QualityRating(@"8500",@"")
            },
            {
                9,
                new QualityRating(@"7000",@"")
            },
            {
                8,
                new QualityRating(@"5500",@"1920x1080 FHD-HDR")
            },
            {
                7,
                new QualityRating(@"4250",@"1280x720 HD-SDR")
            },
            {
                6,
                new QualityRating(@"3000",@"1280x720 HD-SDR")
            },
            {
                5,
                new QualityRating(@"2400",@"1280x720 HD-SDR")
            },
            {
                4,
                new QualityRating(@"1800",@"854x480 SD-SDR")
            },
            {
                3,
                new QualityRating(@"1200",@"854x480 nHD-SDR")
            },
            {
                2,
                new QualityRating(@"800",@"640x360 nHD-SDR")
            },
            {
                1,
                new QualityRating(@"480",@"640x360 nHD-SDR")
            },
            {
                0,
                new QualityRating(@"400",@"640x360 nHD-SDR")
            }
        };

        public static Dictionary<int, QualityRating> AudioDefinitions { get; } = new Dictionary<int, QualityRating>
        {
            {
                8,
                new QualityRating(@"1000",@"1Mbps")
            },
            {
                7,
                new QualityRating(@"640",@"640kbps")
            },
            {
                6,
                new QualityRating(@"512",@"512kbps")
            },
            {
                5,
                new QualityRating(@"480",@"480kbps")
            },
            {
                4,
                new QualityRating(@"400",@"400kbps")
            },
            {
                3,
                new QualityRating(@"300",@"300kbps")
            },
            {
                2,
                new QualityRating(@"256",@"256kbps")
            },
            {
                1,
                new QualityRating(@"128",@"128kbps")
            },
            {
                0,
                new QualityRating(@"64",@"64kbps")
            }
        };
    }
}
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
                new QualityRating(@"8500k",@"")
            },
            {
                9,
                new QualityRating(@"7000k",@"")
            },
            {
                8,
                new QualityRating(@"5500k",@"1920x1080 FHD-HDR")
            },
            {
                7,
                new QualityRating(@"4250k",@"1280x720 HD-SDR")
            },
            {
                6,
                new QualityRating(@"3000k",@"1280x720 HD-SDR")
            },
            {
                5,
                new QualityRating(@"2400k",@"1280x720 HD-SDR")
            },
            {
                4,
                new QualityRating(@"1800k",@"854x480 SD-SDR")
            },
            {
                3,
                new QualityRating(@"1200k",@"854x480 nHD-SDR")
            },
            {
                2,
                new QualityRating(@"800k",@"640x360 nHD-SDR")
            },
            {
                1,
                new QualityRating(@"480k",@"640x360 nHD-SDR")
            },
            {
                0,
                new QualityRating(@"400k",@"640x360 nHD-SDR")
            }
        };

        public static Dictionary<int, QualityRating> AudioDefinitions { get; } = new Dictionary<int, QualityRating>
        {
            {
                8,
                new QualityRating(@"1000k",@"1Mbps")
            },
            {
                7,
                new QualityRating(@"640k",@"640kbps")
            },
            {
                6,
                new QualityRating(@"512k",@"512kbps")
            },
            {
                5,
                new QualityRating(@"480k",@"480kbps")
            },
            {
                4,
                new QualityRating(@"400k",@"400kbps")
            },
            {
                3,
                new QualityRating(@"300k",@"300kbps")
            },
            {
                2,
                new QualityRating(@"256k",@"256kbps")
            },
            {
                1,
                new QualityRating(@"128k",@"128kbps")
            },
            {
                0,
                new QualityRating(@"64k",@"64kbps")
            }
        };
    }
}
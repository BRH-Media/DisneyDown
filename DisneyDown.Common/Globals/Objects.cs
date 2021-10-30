using DisneyDown.Common.KeySystem;
using DisneyDown.Common.Schemas;

namespace DisneyDown.Common.Globals
{
    public static class Objects
    {
        /// <summary>
        /// Current video quality that was matched
        /// </summary>
        public static QualityRating VideoQuality { get; set; } = new QualityRating(@"", @"");

        /// <summary>
        /// Current key server connection provider; maintains credentials and endpoint methods
        /// </summary>
        public static Connection KeyServerConnection { get; set; } = new Connection();

        /// <summary>
        /// Current bandwidth definitions; maintains quality filtering information
        /// </summary>
        public static BandwidthDefinitions CurrentBandwidthDefinitions { get; set; } = BandwidthDefinitions.GetBandwidthDefinitions();
    }
}
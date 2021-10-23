using DisneyDown.Common.KeySystem;
using DisneyDown.Common.Parsers;

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
    }
}
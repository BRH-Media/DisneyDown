using DisneyDown.Common.Globals.FilterValues;

namespace DisneyDown.Common.Globals
{
    public static class Verification
    {
        /// <summary>
        /// Values used to aid in filtering out incorrect segments
        /// </summary>
        public static FilterValuesProvider SegmentFilter => FilterValuesProvider.Init();
    }
}
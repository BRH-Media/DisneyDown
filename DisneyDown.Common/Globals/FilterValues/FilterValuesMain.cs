namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesMain
    {
        public FilterValuesItem VideoFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter = @"-MAIN/",
            SecondaryFilter = @"MAIN",
            FallbackFilter = @".mp4"
        };

        public FilterValuesItem AudioFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter = @"-MAIN/",
            SecondaryFilter = @"MAIN",
            FallbackFilter = @".mp4a"
        };

        public FilterValuesItem SubtitlesFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter = @"-MAIN/",
            SecondaryFilter = @"MAIN",
            FallbackFilter = @".vtt"
        };
    }
}
namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesMain
    {
        /// <summary>
        /// Filters used for video content
        /// </summary>
        public FilterValuesItem VideoFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"-MAIN/",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"MAIN",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @".mp4",
                Enabled = false
            }
        };

        /// <summary>
        /// Filters used for audio content
        /// </summary>
        public FilterValuesItem AudioFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"-MAIN/",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"MAIN",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @".mp4a",
                Enabled = false
            }
        };

        /// <summary>
        /// Filters used for subtitles
        /// </summary>
        public FilterValuesItem SubtitlesFilter { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"-MAIN/",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"MAIN",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @".vtt",
                Enabled = false
            }
        };
    }
}
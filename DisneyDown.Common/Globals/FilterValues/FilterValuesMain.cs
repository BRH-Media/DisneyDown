using Newtonsoft.Json;

namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesMain
    {
        /// <summary>
        /// Returns true if one or more filters have their Fallback filter strings enabled
        /// </summary>
        [JsonIgnore]
        public bool FallbackTriggered =>
            VideoFilter.FallbackFilter.Enabled || AudioFilter.FallbackFilter.Enabled || SubtitlesFilter.FallbackFilter.Enabled;

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
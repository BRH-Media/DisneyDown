using Newtonsoft.Json;

namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesAuxiliary
    {
        /// <summary>
        /// Returns true if one or more filters have their Fallback filter strings enabled
        /// </summary>
        [JsonIgnore]
        public bool FallbackTriggered =>
            DubCard.FallbackFilter.Enabled || BumperIntro.FallbackFilter.Enabled;

        /// <summary>
        /// "Dub Card" filters make sure no irrelevant segments are processed
        /// </summary>
        public FilterValuesItem DubCard { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @"DUB_CARD",
                Enabled = false
            }
        };

        /// <summary>
        /// "Bumper intro" filters ensure that the Disney+ Originals intro is detected properly
        /// </summary>
        public FilterValuesItem BumperIntro { get; set; } = new FilterValuesItem
        {
            PrimaryFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = true
            },
            SecondaryFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = true
            },
            FallbackFilter =
            {
                FilterString = @"-BUMPER/",
                Enabled = false
            },
        };
    }
}
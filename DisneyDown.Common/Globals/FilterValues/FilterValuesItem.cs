namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesItem
    {
        /// <summary>
        /// This value is matched first
        /// </summary>
        public FilterValuesItemEntry PrimaryFilter { get; set; } = new FilterValuesItemEntry();

        /// <summary>
        /// This value is matched second but only if the primary filter wasn't
        /// </summary>
        public FilterValuesItemEntry SecondaryFilter { get; set; } = new FilterValuesItemEntry();

        /// <summary>
        /// This value is only matched if it is explicitly enabled (it causes a lot of issues by default)
        /// </summary>
        public FilterValuesItemEntry FallbackFilter { get; set; } = new FilterValuesItemEntry();

        /// <summary>
        /// Represents the filter as PRIMARY|SECONDARY|FALLBACK
        /// </summary>
        public string StringRepresentation
            => $"{PrimaryFilter}|{SecondaryFilter}|{FallbackFilter}";
    }
}
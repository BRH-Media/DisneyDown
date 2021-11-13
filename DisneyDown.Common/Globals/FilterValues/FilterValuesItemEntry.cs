namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesItemEntry
    {
        /// <summary>
        /// This is what will be used to perform a '%LIKE%' match
        /// </summary>
        public string FilterString { get; set; } = @"";

        /// <summary>
        /// This determines whether this particular value will be utilised
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}
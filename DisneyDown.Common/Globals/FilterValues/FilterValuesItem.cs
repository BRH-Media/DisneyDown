namespace DisneyDown.Common.Globals.FilterValues
{
    public class FilterValuesItem
    {
        public string PrimaryFilter { get; set; } = @"";
        public string SecondaryFilter { get; set; } = @"";
        public string FallbackFilter { get; set; } = @"";

        public string GetStringRepresentation()
            => $"{PrimaryFilter}|{SecondaryFilter}|{FallbackFilter}";
    }
}
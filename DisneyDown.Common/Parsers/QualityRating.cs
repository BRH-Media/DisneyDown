namespace DisneyDown.Common.Parsers
{
    public class QualityRating
    {
        public string SearchCriteria { get; }
        public string QualityName { get; }

        public QualityRating(string criteria, string name)
        {
            SearchCriteria = criteria;
            QualityName = name;
        }
    }
}
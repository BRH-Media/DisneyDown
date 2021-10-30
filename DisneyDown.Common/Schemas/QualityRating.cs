namespace DisneyDown.Common.Schemas
{
    public class QualityRating
    {
        public string SearchCriteria { get; set; }
        public string QualityName { get; set; }

        public QualityRating()
        {
            //blank initialiser
        }

        public QualityRating(string criteria, string name)
        {
            SearchCriteria = criteria;
            QualityName = name;
        }
    }
}
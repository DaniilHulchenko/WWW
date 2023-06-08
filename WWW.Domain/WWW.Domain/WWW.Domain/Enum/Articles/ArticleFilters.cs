namespace WWW.Domain.Enum.Articles
{
    public class ArticleFilters
    {

        public enum status
        {
            Expected,
            Onsale,
            Passed,
        }

        public enum date
        {
            Today,
            This_Week,
            This_Weekends,
        }
    }
}

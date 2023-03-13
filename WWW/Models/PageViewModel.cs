namespace WWW.Models
{
    public class PageViewModel
    {
        public int СurrentlyPageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            СurrentlyPageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (СurrentlyPageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (СurrentlyPageNumber < TotalPages);
            }
        }
    }
}

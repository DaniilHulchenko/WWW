namespace WWW.Models
{
    public class PageIndexViewModel<T>
    {
        public PageIndexViewModel(IEnumerable<T> data, int pageSize, int СurrentlyPageNumber)
        {
            if (data!=null)
            {
                Data = data.Skip((СurrentlyPageNumber - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel = new PageViewModel(data.Count(), СurrentlyPageNumber, pageSize);
            }
        }
        public IEnumerable<T> Data { get; set; }
        public PageViewModel PageViewModel { get; set; }

    }
}

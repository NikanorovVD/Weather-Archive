namespace ServiceLayer.Models
{
    public class PageDto<T>
    {
        public IEnumerable<T> Values { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }
}

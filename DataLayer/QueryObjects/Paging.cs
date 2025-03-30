namespace DataLayer.QueryObjects
{
    public class Page<T>
    {
        public IQueryable<T> Query { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
    }

    public static class Paging
    {
        public static Page<T> Page<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize  must be more then zero.");
            if (pageNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageNumber cannot be negative.");
            
            return new Page<T>
            {
                PageSize = pageSize,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)query.Count() / pageSize),
                Query = query.Skip(pageNumber * pageSize).Take(pageSize)
            };
        }
    }
}

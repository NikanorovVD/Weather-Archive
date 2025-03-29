namespace DataLayer.QueryObjects
{
    public static class Paging
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageSize, int pageNumber)
        {
            if (pageSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageSize  must be more then zero.");
            if (pageNumber < 0)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "pageNumber cannot be negative.");

            return query.Skip(pageNumber * pageSize).Take(pageSize);
        }
    }
}

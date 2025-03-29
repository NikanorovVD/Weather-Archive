using System.Linq.Expressions;

namespace ServiceLayer.Abstractions
{
    public interface ICrudService<TEntity, TDto>
    {
        public Task CteateRangeAsync(IEnumerable<TEntity> entities);
        public Task<IEnumerable<TDto>> GetPageWhereAsync
            (Expression<Func<TEntity, bool>> filter, int pageSize, int pageNumber, CancellationToken cancellationToken);
    }
}

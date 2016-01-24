namespace Qodecube.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMongoAsyncDbSet<T> : IQueryable<T>
    {
        Task AddAsync(IEnumerable<T> entities);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity, Expression<Func<T, bool>> expression);

        Task DeleteAsync(Expression<Func<T, bool>> expression);
    }
}

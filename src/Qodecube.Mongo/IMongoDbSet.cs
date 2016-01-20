namespace Qodecube.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IMongoDbSet<T>
    {
        void Add(IEnumerable<T> entities);

        Task AddAsync(IEnumerable<T> entities);

        void Add(T entity);

        Task AddAsync(T entity);

        void Update(T entity, Expression<Func<T, bool>> expression);

        Task UpdateAsync(T entity, Expression<Func<T, bool>> expression);

        void Delete(Expression<Func<T, bool>> expression);

        Task DeleteAsync(Expression<Func<T, bool>> expression);
    }
}

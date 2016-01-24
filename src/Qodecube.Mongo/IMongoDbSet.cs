namespace Qodecube.Mongo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IMongoDbSet<T> : IQueryable<T>
    {
        void Add(IEnumerable<T> entities);
        
        void Add(T entity);

        void Update(T entity, Expression<Func<T, bool>> expression);

        void Delete(Expression<Func<T, bool>> expression);
    }
}

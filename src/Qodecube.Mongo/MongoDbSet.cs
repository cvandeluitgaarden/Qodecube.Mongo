namespace Qodecube.Mongo
{
    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class MongoDbSet<T> : IQueryable<T>, IMongoDbSet<T>
    {
        private IMongoCollection<T> collection;
        private IQueryable<T> innerObject;

        public MongoDbSet(IMongoCollection<T> collection)
        {
            this.collection = collection;
            this.innerObject = collection.AsQueryable();
        }

        public Type ElementType
        {
            get
            {
                return this.innerObject.ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this.innerObject.Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.innerObject.Provider;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.innerObject.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.innerObject.GetEnumerator();
        }

        public void Add(T entity)
        {
            this.AddAsync(entity).Wait();
        }

        public void Add(IEnumerable<T> entities)
        {
            this.AddAsync(entities).Wait();
        }

        public async Task AddAsync(IEnumerable<T> entities)
        {
            await this.collection.InsertManyAsync(entities);
        }

        public void Update(T entity, Expression<Func<T, bool>> expression)
        {
            this.UpdateAsync(entity, expression).Wait();
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            this.DeleteAsync(expression).Wait();
        }

        public async Task AddAsync(T entity)
        {
            await this.collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity, Expression<Func<T, bool>> expression)
        {
            DebugExpression("Update", expression);
            await this.collection.ReplaceOneAsync(expression, entity);
        }

        public async Task DeleteAsync(Expression<Func<T, bool>> expression)
        {
            DebugExpression("Delete", expression);
            await this.collection.DeleteManyAsync(expression);
        }

        private void DebugExpression(string name, Expression<Func<T, bool>> expression)
        {
#if DEBUG

            Console.WriteLine(name + ": " + ((IMongoQueryable)this.innerObject.Where(expression)).GetExecutionModel().ToString());
#endif
        }
    }
}

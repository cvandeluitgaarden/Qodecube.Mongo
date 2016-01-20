namespace Qodecube.Mongo
{
    using MongoDB.Driver;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class MongoDbSet<T> : IQueryable<T>
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
            this.collection.InsertOneAsync(entity).Wait();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

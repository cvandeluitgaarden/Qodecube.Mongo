namespace Qodecube.Mongo
{
    using System.Collections.Generic;
    using System.Dynamic;
    using MongoDB.Driver;

    public class DynamicMongoDbContext : MongoDbContext
    {
        private ExpandoObject collections = new ExpandoObject();

        public DynamicMongoDbContext(string nameOrConnectionstring) : base(nameOrConnectionstring)
        {
            
        }

        internal override void InstantiateMongoCollections(IMongoDatabase database)
        {
            var collections = database.ListCollections().ToList();
        }
    }
}
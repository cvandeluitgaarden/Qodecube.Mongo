namespace Qodecube.Mongo
{
    using MongoDB.Driver;
    using Attributes;
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    
    public abstract class MongoDbContext
    {
        public MongoDbContext(string nameOrConnectionstring)
        {
            var mongoUrl = new MongoUrl(GetConnenctionUrl(nameOrConnectionstring));
            InstantiateMongoCollections(new MongoClient(mongoUrl).GetDatabase(mongoUrl.DatabaseName));
        }

        internal virtual void InstantiateMongoCollections(IMongoDatabase database)
        {
            MethodInfo getCollectionMethod = database.GetType().GetMethod("GetCollection");
            foreach(var dbSetType in this.GetType().GetProperties()
                .Where(prop => prop.PropertyType.IsGenericType && 
                IsValidTypeDefinition(prop.PropertyType.GetGenericTypeDefinition())))
            {
                var argument = dbSetType.PropertyType.GetGenericArguments()[0];
                var collection = getCollectionMethod.MakeGenericMethod(argument)?.Invoke(database, new object[] { GetCollectionName(dbSetType), null });
                var dbSet = CreateMongoDbSet(collection, argument);
                dbSetType.SetValue(this, dbSet);
            }
        }

        private bool IsValidTypeDefinition(Type type)
        {
            if(type == typeof(MongoDbSet<>))
            {
                return true;
            }
            var interfaces = typeof(MongoDbSet<>).GetInterfaces();
            return interfaces.Where(x => x.ToString() == type.ToString()).Any();
        }

        private object CreateMongoDbSet(object collection, Type type)
        {
            var mongoDbSetType = typeof(MongoDbSet<>).MakeGenericType(type);
            return Activator.CreateInstance(mongoDbSetType, collection);
        }

        private string GetCollectionName(PropertyInfo collectionType)
        {
            var attribute = collectionType.GetCustomAttribute<CollectionNameAttribute>();
            if(attribute != null)
            {
                return attribute.Name;
            }

            return collectionType.Name;
        }

        private string GetConnenctionUrl(string nameOrConnectionstring)
        {
            if (!nameOrConnectionstring.StartsWith("mongodb://", StringComparison.OrdinalIgnoreCase))
            {
                nameOrConnectionstring = ConfigurationManager.ConnectionStrings[nameOrConnectionstring].ConnectionString;
            }

            return nameOrConnectionstring;
        }
    }
}

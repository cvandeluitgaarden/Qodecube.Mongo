namespace Qodecube.Mongo
{
    using MongoDB.Driver;
    
    public interface IMongoManagement<T>
    {
        IMongoCollection<T> Collection { get; }
    }
}

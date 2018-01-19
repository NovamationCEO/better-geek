using MongoDB.Driver;

namespace BetterGeekApi.Database
{
    public interface IDatabaseFactory
    {

        IMongoDatabase Create();
        IMongoCollection<T> GetCollection<T>();

        IMongoCollection<T> GetCollection<T>(string collectionName);
    }
}
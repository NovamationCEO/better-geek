using MongoDB.Driver;
using BetterGeekApi.Interfaces;
using Microsoft.Extensions.Options;
using BetterGeekApi.Model;

namespace BetterGeekApi.Database
{
    public class DatabaseFactory : IDatabaseFactory
    {

        private readonly IOptions<Settings> _settings;


        public DatabaseFactory(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        public IMongoDatabase Create()
        {
            return new MongoClient(_settings.Value.ConnectionString).GetDatabase(_settings.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return this.GetCollection<T>(_settings.Value.Database);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            var db = this.Create();
            return db.GetCollection<T>(collectionName);
        }


    }
}
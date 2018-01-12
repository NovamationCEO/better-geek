using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BetterGeekApi.Model;

namespace BetterGeekApi.Data
{
    public class GameContext
    {
        private readonly IMongoDatabase _database = null;

        public GameContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Game> Games
        {
            get
            {
                return _database.GetCollection<Game>("Game");
            }
        }
    }
}

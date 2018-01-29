using System.Collections.Generic;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using System.Linq;

namespace BetterGeekApi.Managers
{
    public class UserManager : EntityManager<User>, IUserManager
    {
        private readonly IGameManager _gameManager;
        private readonly IOptions<Settings> _settings;

        public UserManager(IDatabaseFactory databaseFactory, IGameManager gameManager, IOptions<Settings> settings) : base(databaseFactory)
        {
            _gameManager = gameManager;
            _settings = settings;
        }

        public async Task syncUser(string id)
        {
            var user = await this.GetById(id);

            var bggUserName = user.BGGUserName;

            var client = new HttpClient();
            var uri = new Uri(_settings.Value.BBGConnectionString + bggUserName);

            var response = await client.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            var result = await response.Content.ReadAsStringAsync();

            var games = JsonConvert.DeserializeObject<IEnumerable<Game>>(result);


            foreach (var game in games)
            {
                var storedGame = await _gameManager.GetByGameId(game.GameId);

                if (storedGame != null)
                {
                    await _gameManager.Patch(storedGame.Id, game.ToBsonDocument());
                }
                else
                {
                    await _gameManager.Create(game);
                }

            }

            IEnumerable<int> gameIds = games.Select(game => game.GameId);
            IEnumerable<Game> gameCollection = await _gameManager.GetByGameIds(gameIds);
            IEnumerable<string> gameObjectIds = gameCollection.Select(game => game.Id);

            BsonArray dataFields = new BsonArray(gameObjectIds);

            BsonDocument newUser = new BsonDocument { { "gameCollection", dataFields } };

            await this.Patch(id, newUser);
        }

    }

}
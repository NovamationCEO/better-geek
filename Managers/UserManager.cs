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

namespace BetterGeekApi.Managers
{
    public class UserManager : EntityManager<User>, IUserManager
    {
        private readonly IGameManager _gameManager;

        public UserManager(IDatabaseFactory databaseFactory, IGameManager gameManager) : base(databaseFactory)
        {
            _gameManager = gameManager;
        }

        public async Task syncUser(string id)
        {
            var user = await this.GetById(id);

            var bggUserName = user.BGGUserName;

            var client = new HttpClient();
            var uri = new Uri("https://bgg-json.azurewebsites.net/collection/" + bggUserName);

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
                    await _gameManager.Update(storedGame.Id, game);
                }
                else
                {
                    await _gameManager.Create(game);
                }

            }
        }

    }

}
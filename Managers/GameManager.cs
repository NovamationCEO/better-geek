using System.Collections.Generic;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.Linq;

namespace BetterGeekApi.Managers
{
    public class GameManager : EntityManager<Game>, IGameManager
    {
        private readonly IEntityManager<Game> _entityManager;

        public GameManager(IDatabaseFactory databaseFactory, IEntityManager<Game> entityManager) : base(databaseFactory)
        {
            _entityManager = entityManager;
        }

        public async Task<Game> GetByGameId(int id)
        {
            return await _entityManager.GetByProperty("gameId", id.ToString());

        }


        public async new Task Patch(string id, BsonDocument document)
        {
            document.Remove("gameId");

            await _entityManager.Patch(id, document);
        }

        public async Task<IEnumerable<Game>> GetByGameIds(IEnumerable<int> ids) {

            var stringIds = ids.Select(id => id.ToString());
            return await _entityManager.GetByProperty("gameId", ids);

        }

    }

}
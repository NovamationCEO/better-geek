using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;
using MongoDB.Bson;

namespace BetterGeekApi.Interfaces
{
    public interface IGameManager : IEntityManager<Game>
    {
        Task<Game> GetByGameId(int id);

        Task<IEnumerable<Game>> GetByGameIds(IEnumerable<int> ids);
    }
}
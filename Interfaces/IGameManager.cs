using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;

namespace BetterGeekApi.Interfaces
{
    public interface IGameManager : IEntityManager<Game>
    {
        Task<Game> GetByGameId(int id);
    }
}
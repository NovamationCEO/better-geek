using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;

namespace BetterGeekApi.Interfaces
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> Get();

        Task<Game> GetById(string id);

        Task Create(Game item);

        Task<bool> Remove(string id);

        Task<bool> Update(string id, Game item);

    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;

namespace BetterGeekApi.Interfaces
{
    public interface IEntityManager<T> where T : Entity
    {
        Task<List<T>> Get();

        T GetById(string id);

        T Create(T entity);

        T Update(string id, T entity);

        bool Remove(string id);
    }
}
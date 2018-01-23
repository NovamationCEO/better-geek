using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;

namespace BetterGeekApi.Interfaces
{
    public interface IEntityManager<T> where T : Entity
    {
        Task<List<T>> Get();

        Task<T> GetById(string id);

        Task<T> Create(T entity);

        Task<List<T>> CreateMany(List<T> entities);

        Task<T> Update(string id, T entity);

        Task<bool> Remove(string id);

        Task<T> FindByProperty(string property, string value);
    }
}
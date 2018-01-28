using System.Collections.Generic;
using System.Threading.Tasks;
using BetterGeekApi.Model;
using MongoDB.Bson;

namespace BetterGeekApi.Interfaces
{
    public interface IEntityManager<T> where T : Entity
    {
        Task<IEnumerable<T>> Get();

        Task<T> GetById(string id);

        Task<IEnumerable<T>> GetByIds(IEnumerable<string> ids);

        Task<T> Create(T entity);

        Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities);

        Task<bool> Remove(string id);

        Task<T> GetByProperty(string property, string value);

        Task<IEnumerable<T>> GetByProperty(string propert, IEnumerable<string> values);

        Task Patch(string id, BsonDocument document);
    }
}

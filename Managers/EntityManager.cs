using System.Collections.Generic;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;

namespace BetterGeekApi.Managers
{
    public class EntityManager<T> : IEntityManager<T> where T : Entity
    {
        private readonly IDatabaseFactory _databaseFactory;

        public EntityManager(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public Task<List<T>> Get()
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return collection.OfType<T>().Find(_ => true).ToListAsync();
        }

        public T GetById(string id)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return collection.OfType<T>().AsQueryable<T>().Where(e => e.Id == new ObjectId(id)).FirstOrDefault();
        }

        public bool Remove(string id)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            DeleteResult actionResult = collection.DeleteOne(Builders<Entity>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public T Create(T entity)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            collection.InsertOne(entity);

            return entity;
        }

        public T Update(string id, T entity)
        {
            return entity;
        }
    }
}
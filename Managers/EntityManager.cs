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

        public async Task<List<T>> Get()
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return await collection.OfType<T>().Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return await collection.OfType<T>().AsQueryable<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> Remove(string id)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            DeleteResult actionResult = await collection.DeleteOneAsync(Builders<Entity>.Filter.Eq("Id", id));

            return actionResult.IsAcknowledged
                && actionResult.DeletedCount > 0;
        }

        public async Task<T> Create(T entity)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<List<T>> CreateMany(List<T> entities) {
            var collection = _databaseFactory.GetCollection<Entity>();

            await collection.InsertManyAsync(entities);

            return entities;
        }


        public async Task<T> Update(string id, T entity)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            var filter = Builders<T>.Filter.Eq("Id", id);

            await collection.OfType<T>().ReplaceOneAsync(filter, entity);

            // should do a get by id or use above line.
            return entity;
        }

        public async Task<T> FindByProperty(string property, string value)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            var filterBuilder = Builders<T>.Filter;
            var filter = filterBuilder.Eq(property, value);

            return await collection.OfType<T>().Find(filter).FirstAsync();
        }
    }
}
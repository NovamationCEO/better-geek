using System.Collections.Generic;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization;
using System;
using System.Collections;

namespace BetterGeekApi.Managers
{
    public class EntityManager<T> : IEntityManager<T> where T : Entity
    {
        private readonly IDatabaseFactory _databaseFactory;

        public EntityManager(IDatabaseFactory databaseFactory)
        {
            _databaseFactory = databaseFactory;
        }

        public async Task<IEnumerable<T>> Get()
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return await collection.OfType<T>().Find(_ => true).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            return await collection.OfType<T>().AsQueryable<T>().Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetByIds(IEnumerable<string> ids)
        {
            var collection = _databaseFactory.GetCollection<Entity>();
            var filter = Builders<T>.Filter.In("Id", ids);

            return await collection.OfType<T>().Find(filter).ToListAsync();
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
            entity.UpdatedDate = DateTime.Now;
            entity.CreateDate = DateTime.Now;

            var collection = _databaseFactory.GetCollection<Entity>();

            await collection.InsertOneAsync(entity);

            return entity;
        }

        public async Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities) {
            foreach (var entity in entities) {
                entity.UpdatedDate = DateTime.Now;
                entity.CreateDate = DateTime.Now;
            }

            var collection = _databaseFactory.GetCollection<Entity>();

            await collection.InsertManyAsync(entities);

            return entities;
        }

        public async Task Patch(string id, BsonDocument document)
        {
            document.Remove("updateDate");
            document.Remove("createDate");
            document.Remove("_id");
            document.SetElement(new BsonElement("updatedDate", DateTime.Now));

            // validate payload;
            try {
                BsonSerializer.Deserialize<T>(document);
            } catch(Exception e) {
                throw e;
            }


            var collection = _databaseFactory.GetCollection<BsonDocument>();

            var filter = Builders<BsonDocument>.Filter.Eq("_id", new ObjectId(id));
            var update = new BsonDocumentUpdateDefinition<BsonDocument>(new BsonDocument("$set", document));

            await collection.UpdateOneAsync(filter, update);
        }

        public async Task<T> GetByProperty(string property, string value)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            var filter = Builders<T>.Filter.Eq(property, value);

            return await collection.OfType<T>().Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetByProperty(string property, IEnumerable<string> values)
        {
            var collection = _databaseFactory.GetCollection<Entity>();

            var filter = Builders<T>.Filter.In(property, values);

            return await collection.OfType<T>().Find(filter).ToListAsync();
        }
    }
}
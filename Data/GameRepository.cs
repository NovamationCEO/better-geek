using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using MongoDB.Bson;

namespace BetterGeekApi.Data
{
    public class GameRepository : IGameRepository
    {
        private readonly GameContext _context = null;

        public GameRepository(IOptions<Settings> settings)
        {
            _context = new GameContext(settings);
        }

        public async Task<IEnumerable<Game>> Get()
        {
            try
            {
                return await _context.Games.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Game> GetById(string id)
        {
            var filter = Builders<Game>.Filter.Eq("Id", id);

            try
            {
                return await _context.Games
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task Create(Game item)
        {
            try
            {
                await _context.Games.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Games.DeleteOneAsync(
                     Builders<Game>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }


        public async Task<bool> Update(string id, Game item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Games
                                                .ReplaceOneAsync(n => n.Id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

    }
}

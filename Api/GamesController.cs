using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Infrastructure;
using System;
using System.Collections.Generic;
using MongoDB.Bson;

namespace BetterGeekApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameRepository _gameRepository;

        public GamesController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [NoCache]
        [HttpGet]
        public async Task<IEnumerable<Game>> Get()
        {
            return await _gameRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<Game> GetById(string id)
        {
            return await _gameRepository.GetById(id);
        }

        [HttpPost]
        public void Create([FromBody]Game game)
        {
            _gameRepository.Create(game);
        }

        [HttpPut("{id}")]
        public void Patch(string id, [FromBody]Game game)
        {
            game.Id = new ObjectId(id);
            _gameRepository.Update(id, game);
        }

        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _gameRepository.Remove(id);
        }
    }
}

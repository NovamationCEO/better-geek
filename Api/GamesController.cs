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
        private readonly IGameManager _gameManager;

        public GamesController(IGameManager gameManager)
        {
            _gameManager = gameManager;
        }

        [NoCache]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var games = await _gameManager.Get();

            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var game = await _gameManager.GetById(id);

            if(game == null) {
                return NotFound();
            }

            return Ok(game);

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Game game)
        {
            var newGame = await _gameManager.Create(game);

            return Ok(newGame);
        }

/*        [HttpPut("{id}")]
        public void Patch(string id, [FromBody]Game game)
        {
            game.Id = new ObjectId(id);
            _gameManager.Update(id, game);
        }
*/
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _gameManager.Remove(id);

            return Ok();
        }

    }
}

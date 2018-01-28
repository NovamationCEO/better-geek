using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Infrastructure;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;

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
            var newObject = new ObjectId();
            var isObjectId = ObjectId.TryParse(id, out newObject);

            if (isObjectId) {
                var game = await _gameManager.GetById(id);

                if (game == null)
                {
                    return NotFound();
                }

                return Ok(game);
            }

            if (id.Contains(","))
            {
                var ids = id.Split(",");

                var games = await _gameManager.GetByIds(ids);

                return Ok(games);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Game game)
        {
            var newGame = await _gameManager.Create(game);

            return Ok(newGame);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]JObject json)
        {
            BsonDocument bsonDocument = BsonSerializer.Deserialize<BsonDocument>(json.ToString());

            try
            {
                await _gameManager.Patch(id, bsonDocument);
            }
            catch (Exception)
            {
                return BadRequest();
            }

            var result = await _gameManager.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try {
                await _gameManager.Remove(id);
            } catch(Exception) {
                return BadRequest();
            }

            return Ok();
        }

    }
}

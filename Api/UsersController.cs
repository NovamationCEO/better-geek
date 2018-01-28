using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BetterGeekApi.Interfaces;
using BetterGeekApi.Model;
using BetterGeekApi.Infrastructure;
using System;
using System.Collections.Generic;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using MongoDB.Bson.Serialization;

namespace BetterGeekApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [NoCache]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userManager.Get();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userManager.GetById(id);

            if(user == null) {
                return NotFound();
            }

            return Ok(user);

        }

        [HttpGet("{id}/sync")]
        public async Task<IActionResult> Sync(string id)
        {
            await _userManager.syncUser(id);

            return Ok();

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]User user)
        {
            var newuser = await _userManager.Create(user);

            return Ok(newuser);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody]JObject json)
        {
            BsonDocument bsonDocument = BsonSerializer.Deserialize<BsonDocument>(json.ToString());

            try {
                await _userManager.Patch(id, bsonDocument); 
            } catch(Exception) {
                return BadRequest();
            }

            var result = await _userManager.GetById(id);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userManager.Remove(id);

            return Ok();
        }

    }
}

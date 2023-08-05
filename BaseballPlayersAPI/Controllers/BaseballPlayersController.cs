using BaseballPlayersAPI.Models;
using BaseballPlayersAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BaseballPlayersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseballPlayersController : ControllerBase
    {
        private readonly IBaseballPlayersService _baseballPlayersService;

        public BaseballPlayersController(IBaseballPlayersService baseballPlayersService)
        {
            _baseballPlayersService = baseballPlayersService;
        }
        
        // GET: api/BaseballPlayers
        [HttpGet]
        public async Task<List<PlayerInfo>> Get(string bats="", string proTeam="")
        {
            return await _baseballPlayersService.GetPlayers(bats, proTeam);
        }
        

        // GET: api/BaseballPlayers/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<PlayerInfo> Get(int id)
        {
            return await _baseballPlayersService.GetPlayer(id);
        }

        // POST: api/BaseballPlayers
        [HttpPost]
        public async Task<ActionResult<PlayerInfo>> Post([FromBody] PlayerInfo playerInfo)
        {
            //var player = JsonConvert.DeserializeObject<PlayerInfo>(value);
            return await _baseballPlayersService.AddPlayer(playerInfo);
        }

        // PUT: api/BaseballPlayers/5
        [HttpPut]
        public async Task<ActionResult<PlayerInfo>> Put([FromBody] PlayerInfo player)
        {
            return await _baseballPlayersService.UpdatePlayer(player);
        }

        // DELETE: api/BaseballPlayers/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await _baseballPlayersService.DeletePlayer(id);
        }
    }
}

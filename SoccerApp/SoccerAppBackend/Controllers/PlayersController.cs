using System.Numerics;
using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private readonly IPlayersService playersService;

        public PlayersController(IPlayersService playersService)
        {
            this.playersService = playersService;
        }


        public async Task<IActionResult> GetActivePlayers()
        {
            List<PlayerDto> activePlayers = await playersService.GetActivePlayers();

            return Ok(activePlayers);
        }


        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayerById(int playerId)
        {
            PlayerDto player = await playersService.GetPlayerById(playerId);

            if (player.PlayerId > 0)
            {
                return Ok(player);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreatePlayer(PlayerDto playerToCreate)
        {
            PlayerDto newPlayer = await playersService.CreatePlayer(playerToCreate);

            if (newPlayer.PlayerId > 0)
            {
                return Ok(newPlayer);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{playerId}")]
        public async Task<IActionResult> UpdatePlayer(PlayerDto playerToUpdate)
        {
            playerToUpdate = await playersService.UpdatePlayer(playerToUpdate);

            return Ok(playerToUpdate);
        }


        [HttpDelete("{playerId}")]
        public async Task<IActionResult> DeactivatePlayerById(int playerId)
        {
            PlayerDto playerToDeactivate = await playersService.DeactivatePlayerById(playerId);

            return Ok(playerToDeactivate);
        }
    }
}

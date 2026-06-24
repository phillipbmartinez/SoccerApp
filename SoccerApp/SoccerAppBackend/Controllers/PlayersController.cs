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
    }
}

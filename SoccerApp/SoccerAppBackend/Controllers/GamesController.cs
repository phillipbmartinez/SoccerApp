using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGamesService gamesService;

        public GamesController(IGamesService gamesService)
        {
            this.gamesService = gamesService;
        }

        public async Task<IActionResult> GetAllGames()
        {
            List<GameDto> activeGames = await gamesService.GetAllGames();

            if (activeGames.Count > 0)
            {
                return Ok(activeGames);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            GameDto game = await gamesService.GetGameById(gameId);

            if (game.GameId != 0)
            {
                return Ok(game);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateGame(GameDto gameToCreate)
        {
            GameDto game = await gamesService.CreateGame(gameToCreate);

            if (game.GameId != 0)
            {
                return Ok(game);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{gameId}")]
        public async Task<IActionResult> UpdateGame(GameDto gameToUpdate)
        {
            gameToUpdate = await gamesService.UpdateGame(gameToUpdate);

            return Ok(gameToUpdate);
        }
    }
}

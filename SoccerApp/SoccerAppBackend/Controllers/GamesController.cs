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
        private readonly IGamesTeamsOpponentsService gamesTeamsOpponentsService;

        public GamesController(IGamesService gamesService, IGamesTeamsOpponentsService gamesTeamsOpponentsService)
        {
            this.gamesService = gamesService;
            this.gamesTeamsOpponentsService = gamesTeamsOpponentsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGames()
        {
            List<GameTeamOpponentDto> games = await gamesTeamsOpponentsService.GetGames();

            return Ok(games);
        }

        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcomingGames()
        {
            List<GameTeamOpponentDto> upcomingGames = await gamesTeamsOpponentsService.GetUpcomingGames();

            return Ok(upcomingGames);
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

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

        [HttpGet("previous")]
        public async Task<IActionResult> GetPreviousGames()
        {
            List<GameTeamOpponentDto> previousGames = await gamesTeamsOpponentsService.GetPreviousGames();

            return Ok(previousGames);
        }

        [HttpGet("{gameId}")]
        public async Task<IActionResult> GetGameById(int gameId)
        {
            GameTeamOpponentDto game = await gamesTeamsOpponentsService.GetGameByGameId(gameId);

            if (game.GameId != 0)
            {
                return Ok(game);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("team/{teamId}")]
        public async Task<IActionResult> GetTeamGamesByTeamId(int teamId)
        {
            List<GameTeamOpponentDto> teamGames = await gamesTeamsOpponentsService.GetGamesByTeamId(teamId);

            return Ok(teamGames);
        }

        [HttpGet("team/{teamId}/previous")]
        public async Task<IActionResult> GetTeamsPreviousGames(int teamId)
        {
            List<GameTeamOpponentDto> teamGames = await gamesTeamsOpponentsService.GetTeamsPreviousGames(teamId);

            return Ok(teamGames);
        }

        [HttpGet("team/{teamId}/upcoming")]
        public async Task<IActionResult> GetTeamsUpcomingGames(int teamId)
        {
            List<GameTeamOpponentDto> teamGames = await gamesTeamsOpponentsService.GetTeamsUpcomingGames(teamId);

            return Ok(teamGames);
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

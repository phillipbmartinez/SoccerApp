using ArsenalAcademy.Models;
using ArsenalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.Controllers
{
    public class GamesController : Controller
    {
        private readonly IGameTeamOpponentsApiService gameTeamOpponentsApiService;

        public GamesController(IGameTeamOpponentsApiService gameTeamOpponentsApiService)
        {
            this.gameTeamOpponentsApiService = gameTeamOpponentsApiService;
        }

        public async Task<IActionResult> Index()
        {
            List<ViewGameTeamOpponentViewModel> games = await gameTeamOpponentsApiService.GetGames();

            return View(games);
        }

        public async Task<IActionResult> UpcomingGames()
        {
            List<ViewGameTeamOpponentViewModel> upcomingGames = await gameTeamOpponentsApiService.GetUpcomingGames();

            return View(upcomingGames);
        }

        public async Task<IActionResult> PreviousGames()
        {
            List<ViewGameTeamOpponentViewModel> previousGames = await gameTeamOpponentsApiService.GetPreviousGames();

            return View(previousGames);
        }

        public async Task<IActionResult> TeamGames(int id)
        {
            List<ViewGameTeamOpponentViewModel> teamGames = await gameTeamOpponentsApiService.GetGamesByTeamId(id);

            return View(teamGames);
        }

        public async Task<IActionResult> GetGame(int id)
        {
            ViewGameTeamOpponentViewModel game = await gameTeamOpponentsApiService.GetGameById(id);

            return View("Game", game);
        }
    }
}

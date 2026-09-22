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
    }
}

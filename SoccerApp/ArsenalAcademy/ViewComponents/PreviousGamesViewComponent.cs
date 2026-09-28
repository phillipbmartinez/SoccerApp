using ArsenalAcademy.Models;
using ArsenalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.ViewComponents
{
    public class PreviousGamesViewComponent : ViewComponent
    {
        private readonly IGameTeamOpponentsApiService gameTeamOpponentsApiService;

        public PreviousGamesViewComponent(IGameTeamOpponentsApiService gameTeamOpponentsApiService)
        {
            this.gameTeamOpponentsApiService = gameTeamOpponentsApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            List<ViewGameTeamOpponentViewModel> teamsPreviousGames = await gameTeamOpponentsApiService.GetTeamsPreviousGames(teamId);

            return View(teamsPreviousGames);
        }
    }
}

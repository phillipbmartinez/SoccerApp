using ArsenalAcademy.Models;
using ArsenalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.ViewComponents
{
    public class UpcomingGamesViewComponent : ViewComponent
    {
        private readonly IGameTeamOpponentsApiService gameTeamOpponentsApiService;

        public UpcomingGamesViewComponent(IGameTeamOpponentsApiService gameTeamOpponentsApiService)
        {
            this.gameTeamOpponentsApiService = gameTeamOpponentsApiService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int teamId)
        {
            List<ViewGameTeamOpponentViewModel> teamsPreviousGames = await gameTeamOpponentsApiService.GetTeamsUpcomingGames(teamId);

            return View(teamsPreviousGames);
        }
    }
}

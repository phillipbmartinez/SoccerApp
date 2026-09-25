using ArsenalAcademy.Models;
using ArsenalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ITeamsCoachesApiService teamsCoachesApiService;

        public TeamsController(ITeamsCoachesApiService teamsCoachesApiService)
        {
            this.teamsCoachesApiService = teamsCoachesApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ViewTeamCoachViewModel> teams = await teamsCoachesApiService.GetTeams();

            return View(teams);
        }
    }
}

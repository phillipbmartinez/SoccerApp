using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamsController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }


        public async Task<IActionResult> GetActiveTeams()
        {
            List<TeamDto> activeTeams = await teamsService.GetActiveTeams();

            return Ok(activeTeams);
        }

        [HttpGet("{teamId}")]
        public async Task<IActionResult> GetTeamById(int teamId)
        {
            TeamDto team = await teamsService.GetTeamById(teamId);

            if (team.TeamId != 0)
            {
                return Ok(team);
            }
            else
            {
                return NotFound();
            }
        }
    }
}

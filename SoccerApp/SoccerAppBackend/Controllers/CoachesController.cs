using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : Controller
    {
        private readonly IDatabaseService databaseSerivce;
        private readonly ICoachesService coachesService;

        public CoachesController(IDatabaseService databaseSerivce, ICoachesService coachesService)
        {
            this.databaseSerivce = databaseSerivce;
            this.coachesService = coachesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllActiveCoaches()
        {
            List<Coach> coaches = await coachesService.ReturnAllActiveCoaches();

            return Ok(coaches);
        }


        [HttpGet("inactive")]
        public async Task<IActionResult> GetAllInactiveCoaches()
        {
            List<Coach> coaches = await coachesService.ReturnAllInactiveCoaches();

            return Ok(coaches);
        }


        [HttpGet("{coachId}")]
        public async Task<IActionResult> GetCoachById(int coachId)
        {
            Coach coach = await coachesService.ReturnCoachById(coachId);

            if (coach.CoachId > 0)
            {
                return Ok(coach);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{coachId}")]
        public async Task<IActionResult> DeleteCoachById(int coachId)
        {
            Coach coacheToDelete = await coachesService.DeleteCoachById(coachId);

            return Ok(coacheToDelete);
        }
    }
}

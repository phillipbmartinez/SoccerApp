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
        public async Task<IActionResult> GetAllCoaches()
        {
            List<Coach> coaches = await coachesService.ReturnAllCoaches();

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
    }
}

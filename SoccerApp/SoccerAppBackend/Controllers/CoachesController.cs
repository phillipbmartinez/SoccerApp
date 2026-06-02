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
            List<Coach> coaches = await coachesService.GetActiveCoaches();

            return Ok(coaches);
        }


        [HttpGet("inactive")]
        public async Task<IActionResult> GetAllInactiveCoaches()
        {
            List<Coach> coaches = await coachesService.GetInactiveCoaches();

            return Ok(coaches);
        }


        [HttpGet("{coachId}")]
        public async Task<IActionResult> GetCoachById(int coachId)
        {
            Coach coach = await coachesService.GetCoachById(coachId);

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
            Coach coacheToDelete = await coachesService.DeactivateCoachById(coachId);

            return Ok(coacheToDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCoach(Coach coachToUpdate)
        {
            Coach updatedCoach = await coachesService.UpdateCoach(coachToUpdate);

            return Ok(updatedCoach);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoach(Coach coachtoCreate)
        {
            Coach newCoach = await coachesService.CreateCoach(coachtoCreate);

            return Ok(newCoach);
        }
    }
}

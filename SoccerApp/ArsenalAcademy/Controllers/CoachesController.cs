using ArsenalAcademy.Models;
using ArsenalAcademy.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.Controllers
{
    public class CoachesController : Controller
    {
        private readonly ICoachesApiService coachesApiService;

        public CoachesController(ICoachesApiService coachesApiService)
        {
            this.coachesApiService = coachesApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ViewCoachViewModel> coaches = await coachesApiService.GetAllCoaches();

            if (coaches != null && coaches.Count > 0)
            {
                return View(coaches);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCoach(int id)
        {
            ViewCoachViewModel coach = await coachesApiService.GetCoachById(id);

            if (coach != null && coach.CoachId > 0)
            {
                return View(coach);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateCoach()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoach([FromForm] CreateCoachViewModel coachToCreate)
        {
            if (ModelState.IsValid)
            {
                coachToCreate = await coachesApiService.CreateCoach(coachToCreate);

                if (coachToCreate.CoachId > 0 && coachToCreate.UserId > 0)
                {
                    return View("Index");
                }
                else
                {
                    return View(coachToCreate);
                }
            }
            else
            {
                return View(coachToCreate);
            }
        }
    }
}

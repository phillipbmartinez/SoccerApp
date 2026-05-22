using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : Controller
    {
        private readonly IDatabaseService database;

        public CoachesController(IDatabaseService database)
        {
            this.database = database;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

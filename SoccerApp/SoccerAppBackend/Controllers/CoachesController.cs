using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : Controller
    {
        private readonly IDatabase database;

        public CoachesController(IDatabase database)
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

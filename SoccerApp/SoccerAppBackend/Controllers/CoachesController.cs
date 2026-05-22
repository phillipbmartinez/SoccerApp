using Microsoft.AspNetCore.Mvc;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CoachesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : Controller
    {
        public TeamsController()
        {
        
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}

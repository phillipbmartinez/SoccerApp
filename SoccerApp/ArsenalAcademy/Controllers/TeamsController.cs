using Microsoft.AspNetCore.Mvc;

namespace ArsenalAcademy.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

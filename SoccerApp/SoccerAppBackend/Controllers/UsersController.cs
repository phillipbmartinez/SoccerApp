using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IDatabaseService databaseService;
        private readonly IUsersService usersService;

        public UsersController(IDatabaseService databaseService, IUsersService usersService)
        {
            this.databaseService = databaseService;
            this.usersService = usersService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

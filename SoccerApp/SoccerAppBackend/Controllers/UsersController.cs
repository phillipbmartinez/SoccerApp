using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

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

        [HttpGet]
        public async Task<IActionResult> GetActiveUsers()
        {
            List<User> users = await usersService.GetActiveUsers();

            return Ok(users);
        }
    }
}

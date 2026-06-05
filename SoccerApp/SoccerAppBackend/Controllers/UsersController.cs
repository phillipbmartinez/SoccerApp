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


        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactiveUsers()
        {
            List<User> inactiveUsers = await usersService.GetInactiveUsers();

            return Ok(inactiveUsers);
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            User user = await usersService.GetUserById(userId);

            if (user.IsActive == true)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeactivateUser(int userId)
        {
            User userToDelete = await usersService.DeactivateUserById(userId);

            if (userToDelete.IsActive == false)
            {
                return Ok(userToDelete);
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(User userToUpdate)
        {
            User updatedUser = await usersService.UpdateUser(userToUpdate);

            return Ok(updatedUser);
        }
    }
}

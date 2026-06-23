using Microsoft.AspNetCore.Mvc;
using SoccerAppBackend.Data;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class RolesController : Controller
    {
        private readonly IDatabaseService databaseService;
        private readonly IRolesService rolesService;

        public RolesController(IDatabaseService databaseService, IRolesService rolesService)
        {
            this.databaseService = databaseService;
            this.rolesService = rolesService;
        }


        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            List<RoleDto> roles = await rolesService.GetRoles();

            return Ok(roles);
        }


        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            RoleDto role = await rolesService.GetRoleById(roleId);

            return Ok(role);
        }


        [HttpPut("{roleId}")]
        public async Task<IActionResult> UpdateRole(RoleDto roleToUpdate)
        {
            RoleDto updatedRole = await rolesService.UpdateRole(roleToUpdate);

            return Ok(updatedRole);
        }
    }
}

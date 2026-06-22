using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IRolesService
    {
        Task<RoleDto> GetRoleById(int roleId);
        Task<List<RoleDto>> GetRoles();
    }
}
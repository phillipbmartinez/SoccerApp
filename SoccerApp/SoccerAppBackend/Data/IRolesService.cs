using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IRolesService
    {
        Task<List<RoleDto>> GetRoles();
    }
}
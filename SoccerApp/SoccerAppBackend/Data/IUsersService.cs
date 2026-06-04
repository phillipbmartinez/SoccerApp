using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IUsersService
    {
        Task<List<User>> GetActiveUsers();
    }
}
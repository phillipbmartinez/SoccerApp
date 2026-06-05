using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IUsersService
    {
        Task<List<User>> GetActiveUsers();
        Task<List<User>> GetInactiveUsers();
        Task<User> GetUserById(int userId);
    }
}
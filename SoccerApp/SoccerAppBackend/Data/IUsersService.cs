using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IUsersService
    {
        Task<User> DeactivateUserById(int userId);
        Task<List<User>> GetActiveUsers();
        Task<List<User>> GetInactiveUsers();
        Task<User> GetUserById(int userId);
        Task<User> UpdateUser(User userToUpdate);
    }
}
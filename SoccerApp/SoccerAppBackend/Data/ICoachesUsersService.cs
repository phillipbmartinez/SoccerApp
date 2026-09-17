using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesUsersService
    {
        Task<List<CoachUserDto>> GetActiveCoachesUsers();
        Task<CoachUserDto> GetCoachUserByCoachId(int coachId);
    }
}
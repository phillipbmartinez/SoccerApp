using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesService
    {
        Task<Coach> DeleteCoachById(int coachId);
        Task<List<Coach>> ReturnAllActiveCoaches();
        Task<List<Coach>> ReturnAllInactiveCoaches();
        Task<Coach> ReturnAnyCoachById(int coachId);
        Task<Coach> ReturnCoachById(int coachId);
        Task<Coach> UpdateCoach(Coach coachToUpdate);
    }
}
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesService
    {
        Task<Coach> DeactivateCoachById(int coachId);
        Task<List<Coach>> GetActiveCoaches();
        Task<List<Coach>> GetInactiveCoaches();
        Task<Coach> GetCoachByIdIncludingInactive(int coachId);
        Task<Coach> GetCoachById(int coachId);
        Task<Coach> UpdateCoach(Coach coachToUpdate);
        Task<Coach> CreateCoach(Coach coachToCreate);
    }
}
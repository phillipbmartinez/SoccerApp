using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesService
    {
        Task<List<Coach>> ReturnAllActiveCoaches();
        Task<List<Coach>> ReturnAllInactiveCoaches();
        Task<Coach> ReturnCoachById(int coachId);
    }
}
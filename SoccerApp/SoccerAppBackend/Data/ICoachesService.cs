using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesService
    {
        Task<List<Coach>> ReturnAllActiveCoaches();
        Task<Coach> ReturnCoachById(int coachId);
    }
}
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ICoachesService
    {
        Task<List<Coach>> ReturnAllCoaches();
        Task<Coach> ReturnCoachById(int coachId);
    }
}
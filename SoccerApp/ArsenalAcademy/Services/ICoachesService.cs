using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public interface ICoachesApiService
    {
        Task<List<CoachViewModel>> GetAllCoaches();
        Task<CoachViewModel> GetCoachById(int coachId);
    }
}
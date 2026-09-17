using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public interface ICoachesApiService
    {
        Task<CreateCoachViewModel> CreateCoach(CreateCoachViewModel coachToCreate);
        Task<List<ViewCoachViewModel>> GetAllCoaches();
        Task<ViewCoachViewModel> GetCoachById(int coachId);
    }
}
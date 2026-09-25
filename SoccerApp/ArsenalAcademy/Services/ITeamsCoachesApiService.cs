using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public interface ITeamsCoachesApiService
    {
        Task<List<ViewTeamCoachViewModel>> GetTeams();
    }
}
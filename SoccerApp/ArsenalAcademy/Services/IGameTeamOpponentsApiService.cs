using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public interface IGameTeamOpponentsApiService
    {
        Task<List<ViewGameTeamOpponentViewModel>> GetGames();
        Task<List<ViewGameTeamOpponentViewModel>> GetUpcomingGames();
    }
}
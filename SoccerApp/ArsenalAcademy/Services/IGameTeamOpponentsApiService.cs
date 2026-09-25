using ArsenalAcademy.Models;

namespace ArsenalAcademy.Services
{
    public interface IGameTeamOpponentsApiService
    {
        Task<ViewGameTeamOpponentViewModel> GetGameById(int gameId);
        Task<List<ViewGameTeamOpponentViewModel>> GetGames();
        Task<List<ViewGameTeamOpponentViewModel>> GetGamesByTeamId(int teamId);
        Task<List<ViewGameTeamOpponentViewModel>> GetPreviousGames();
        Task<List<ViewGameTeamOpponentViewModel>> GetUpcomingGames();
    }
}
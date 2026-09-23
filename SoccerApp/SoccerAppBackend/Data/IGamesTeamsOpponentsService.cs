using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IGamesTeamsOpponentsService
    {
        Task<List<GameTeamOpponentDto>> GetGames();
        Task<List<GameTeamOpponentDto>> GetUpcomingGames();
    }
}
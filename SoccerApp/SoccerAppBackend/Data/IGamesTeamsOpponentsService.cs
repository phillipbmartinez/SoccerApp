using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IGamesTeamsOpponentsService
    {
        Task<GameTeamOpponentDto> GetGameByGameId(int gameId);
        Task<List<GameTeamOpponentDto>> GetGames();
        Task<List<GameTeamOpponentDto>> GetGamesByTeamId(int teamId);
        Task<List<GameTeamOpponentDto>> GetPreviousGames();
        Task<List<GameTeamOpponentDto>> GetUpcomingGames();
    }
}
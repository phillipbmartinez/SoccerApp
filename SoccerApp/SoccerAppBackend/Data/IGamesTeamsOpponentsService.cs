using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IGamesTeamsOpponentsService
    {
        Task<GameTeamOpponentDto> GetGameByGameId(int gameId);
        Task<List<GameTeamOpponentDto>> GetGames();
        Task<List<GameTeamOpponentDto>> GetGamesByTeamId(int teamId);
        Task<List<GameTeamOpponentDto>> GetPreviousGames();
        Task<List<GameTeamOpponentDto>> GetTeamsPreviousGames(int teamId);
        Task<List<GameTeamOpponentDto>> GetTeamsUpcomingGames(int teamId);
        Task<List<GameTeamOpponentDto>> GetUpcomingGames();
    }
}
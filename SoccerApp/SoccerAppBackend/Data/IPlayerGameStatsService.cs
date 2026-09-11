using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IPlayerGameStatsService
    {
        Task<PlayerGameStatDto> CreatePlayerGameStat(PlayerGameStatDto playerGameStatToCreate);
        Task<List<PlayerGameStatDto>> GetAllPlayerGameStats();
        Task<PlayerGameStatDto> GetPlayerGameStatById(int playerGameStatId);
        Task<PlayerGameStatDto> UpdatePlayerGameStat(PlayerGameStatDto playerGameStatToUpdate);
    }
}
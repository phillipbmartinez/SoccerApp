using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IPlayersService
    {
        Task<PlayerDto> CreatePlayer(PlayerDto playerToCreate);
        Task<PlayerDto> DeactivatePlayerById(int playerId);
        Task<List<PlayerDto>> GetActivePlayers();
        Task<PlayerDto> GetAnyPlayerById(int playerId);
        Task<PlayerDto> GetActivePlayerById(int playerId);
        Task<PlayerDto> UpdatePlayer(PlayerDto playerToUpdate);
    }
}
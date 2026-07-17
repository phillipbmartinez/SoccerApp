using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IPlayersService
    {
        Task<PlayerDto> CreatePlayer(PlayerDto playerToCreate);
        Task<List<PlayerDto>> GetActivePlayers();
        Task<PlayerDto> GetPlayerById(int playerId);
        Task<PlayerDto> UpdatePlayer(PlayerDto playerToUpdate);
    }
}
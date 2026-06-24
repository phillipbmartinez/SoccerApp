using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IPlayersService
    {
        Task<List<PlayerDto>> GetActivePlayers();
    }
}
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IGamesService
    {
        Task<GameDto> CreateGame(GameDto gameToCreate);
        Task<List<GameDto>> GetAllGames();
        Task<GameDto> GetGameById(int gameId);
        Task<GameDto> UpdateGame(GameDto gameToUpdate);
    }
}
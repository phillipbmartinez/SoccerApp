using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface IOpponentsService
    {
        Task<OpponentDto> CreateOpponent(OpponentDto opponenToCreate);
        Task<OpponentDto> DeactivateOpponent(int opponentId);
        Task<List<OpponentDto>> GetActiveOpponents();
        Task<OpponentDto> GetOpponentById(int opponentId);
        Task<OpponentDto> UpdateOpponent(OpponentDto opponentToUpdate);
    }
}
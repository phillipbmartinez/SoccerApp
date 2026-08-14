using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ITeamsService
    {
        Task<List<TeamDto>> GetActiveTeams();
    }
}
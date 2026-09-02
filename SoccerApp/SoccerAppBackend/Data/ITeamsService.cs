using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public interface ITeamsService
    {
        Task<TeamDto> CreateTeam(TeamDto teamToCreate);
        Task<TeamDto> DeactivateTeam(int teamId);
        Task<List<TeamDto>> GetActiveTeams();
        Task<TeamDto> GetTeamById(int teamId);
        Task<TeamDto> UpdateTeam(TeamDto teamToUpdate);
    }
}
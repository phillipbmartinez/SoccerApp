using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class TeamsService : ITeamsService
    {
        private readonly IDatabaseService databaseService;

        public TeamsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }


        public async Task<List<TeamDto>> GetActiveTeams()
        {
            Console.WriteLine("GetActiveTeams Called");
            List<TeamDto> activeTeams = new List<TeamDto>();
            string sqlQuery = @"SELECT * FROM SoccerAppTeams WHERE IsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    activeTeams.Add(new TeamDto
                    {
                        TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                        TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                        CoachId = reader.IsDBNull(reader.GetOrdinal("CoachId"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("CoachId")),
                        AgeGroup = reader.GetString(reader.GetOrdinal("AgeGroup"))
                    });
                };

                return activeTeams;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => TeamsService => GetActiveTeams: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return activeTeams;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => TeamsService => GetActiveTeams: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return activeTeams;
            }
        }

        public async Task<TeamDto> GetTeamById(int teamId)
        {
            TeamDto team = new TeamDto();
            string sqlQuery = @"SELECT * FROM SoccerAppTeams WHERE TeamId = @teamId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@teamId", teamId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    team.TeamId = reader.GetInt32(reader.GetOrdinal("TeamId"));
                    team.TeamName = reader.GetString(reader.GetOrdinal("TeamName"));
                    team.CoachId = reader.IsDBNull(reader.GetOrdinal("CoachId"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("CoachId"));
                    team.AgeGroup = reader.GetString(reader.GetOrdinal("AgeGroup"));
                }
                ;

                return team;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => TeamsService => GetTeamById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return team;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => TeamsService => GetTeamById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return team;
            }
        }
    }
}

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

        public async Task<TeamDto> CreateTeam(TeamDto teamToCreate)
        {
            TeamDto newTeam = new TeamDto();

            string sqlQuery =
                @"
                    INSERT INTO SoccerAppTeams (TeamName, CoachId, AgeGroup)
                    VALUES (@teamName, @coachId, @AgeGroup)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                if (!string.IsNullOrWhiteSpace(teamToCreate.TeamName.ToString()))
                {
                    command.Parameters.AddWithValue("@teamName", teamToCreate.TeamName);
                }
                else
                {
                    command.Parameters.AddWithValue("@teamName", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(teamToCreate.CoachId?.ToString()))
                {
                    command.Parameters.AddWithValue("@coachId", teamToCreate.CoachId);
                }
                else
                {
                    command.Parameters.AddWithValue("@coachId", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(teamToCreate.AgeGroup?.ToString()))
                {
                    command.Parameters.AddWithValue("@ageGroup", teamToCreate.AgeGroup);
                }
                else
                {
                    command.Parameters.AddWithValue("@ageGroup", DBNull.Value);
                }

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newTeamId))
                {
                    newTeam = await GetTeamById(newTeamId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New Team inserted. ID: {newTeamId} from SoccerAppBackend => TeamsService => CreateTeam");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new Player record in database from SoccerAppBackend => TeamsService => CreateTeam");
                }

                return newTeam;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => TeamsService => CreateTeam: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newTeam;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => TeamsService => CreateTeam: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newTeam;
            }
        }

        public async Task<TeamDto> UpdateTeam(TeamDto teamToUpdate)
        {
            string sqlQuery =
                @"
                    UPDATE SoccerAppTeams
                    SET TeamName = @teamName,
                        CoachId = @coachId,
                        AgeGroup = @ageGroup,
                        ModifiedAt = @modifiedAt
                    WHERE TeamId = @teamId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@teamId", teamToUpdate.TeamId);
                command.Parameters.AddWithValue("@teamName", teamToUpdate.TeamName);
                command.Parameters.AddWithValue("@coachId", teamToUpdate.CoachId);
                command.Parameters.AddWithValue("@AgeGroup", teamToUpdate.AgeGroup);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    teamToUpdate = await GetTeamById(teamToUpdate.TeamId);
                }

                return teamToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => TeamsService => UpdateTeam: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return teamToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => TeamsService => UpdateTeam: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return teamToUpdate;
            }
        }
    }
}

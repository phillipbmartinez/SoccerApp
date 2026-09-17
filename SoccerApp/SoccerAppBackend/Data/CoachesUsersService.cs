using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class CoachesUsersService : ICoachesUsersService
    {
        private readonly IDatabaseService databaseService;

        public CoachesUsersService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<List<CoachUserDto>> GetActiveCoachesUsers()
        {
            List<CoachUserDto> coachesUsers = new List<CoachUserDto>();

            string sqlQuery = "SELECT * FROM vw_SoccerAppCoachesUsers WHERE CoachIsActive = 1 AND UserIsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coachesUsers.Add(new CoachUserDto
                    {
                        CoachId = reader.GetInt32(reader.GetOrdinal("CoachId")),
                        CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("CoachingLicense")),
                        StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("UserId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        TeamName = reader.IsDBNull(reader.GetOrdinal("TeamName"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("TeamName"))
                    });
                };

                return coachesUsers;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllCoaches: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coachesUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coachesUsers;
            }
        }

        public async Task<CoachUserDto> GetCoachUserByCoachId(int coachId)
        {
            CoachUserDto coachUser = new CoachUserDto();

            string sqlQuery = "SELECT * FROM vw_SoccerAppCoachesUsers WHERE CoachId = @coachId AND CoachIsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@coachId", coachId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coachUser.CoachId = reader.GetInt32(reader.GetOrdinal("CoachId"));
                    coachUser.CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CoachingLicense"));
                    coachUser.StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate"));
                    coachUser.UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("UserId"));
                    coachUser.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    coachUser.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    coachUser.Email = reader.GetString(reader.GetOrdinal("Email"));
                    coachUser.TeamName = reader.IsDBNull(reader.GetOrdinal("TeamName"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("TeamName"));
                }

                return coachUser;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesUsersService => ReturnCoachUserByCoachId: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coachUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesUsersService => ReturnCoachUserByCoachId: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coachUser;
            }
        }
    }
}

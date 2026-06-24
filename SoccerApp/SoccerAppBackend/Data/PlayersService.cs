using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class PlayersService
    {
        private readonly IDatabaseService databaseService;

        public PlayersService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }


        public async Task<List<PlayerDto>> GetActivePlayers()
        {
            List<PlayerDto> activePlayers = new List<PlayerDto>();
            string sqlQuery = @"SELECT * FROM SoccerAppPlayers WHERE IsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    activePlayers.Add(new PlayerDto
                    {
                        PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                            ? (int?) null
                            : reader.GetInt32(reader.GetOrdinal("UserId")),
                        TeamId = reader.IsDBNull(reader.GetOrdinal("TeamId"))
                            ? (int?) null
                            : reader.GetInt32(reader.GetOrdinal("TeamId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        JerseyNumber = reader.IsDBNull(reader.GetOrdinal("JerseyNumber"))
                            ? (int?) null
                            : reader.GetInt32(reader.GetOrdinal("JerseyNumber")),
                        DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                            ? (DateTime?) null
                            : reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                    });
                };

                return activePlayers;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => GetPlayers: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return activePlayers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => GetPlayers: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return activePlayers;
            }
        }
    }
}

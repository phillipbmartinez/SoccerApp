using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class PlayersService : IPlayersService
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


        public async Task<PlayerDto> GetPlayerById(int playerId)
        {
            PlayerDto player = new PlayerDto();

            string sqlQuery = @"SELECT * FROM SoccerAppPlayers WHERE PlayerId = @playerId AND IsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@playerId", playerId);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    player.PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId"));
                    player.UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("UserId"));
                    player.TeamId = reader.IsDBNull(reader.GetOrdinal("TeamId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("TeamId"));
                    player.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    player.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    player.JerseyNumber = reader.IsDBNull(reader.GetOrdinal("JerseyNumber"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("JerseyNumber"));
                    player.DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                        ? (DateTime?)null
                        : reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                }
                ;

                return player;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => GetPlayerById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => GetPlayerById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return player;
            }
        }


        public async Task<PlayerDto> GetAnyPlayerById(int playerId)
        {
            PlayerDto player = new PlayerDto();

            string sqlQuery = @"SELECT * FROM SoccerAppPlayers WHERE PlayerId = @playerId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@playerId", playerId);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    player.PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId"));
                    player.UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("UserId"));
                    player.TeamId = reader.IsDBNull(reader.GetOrdinal("TeamId"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("TeamId"));
                    player.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    player.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    player.JerseyNumber = reader.IsDBNull(reader.GetOrdinal("JerseyNumber"))
                        ? (int?)null
                        : reader.GetInt32(reader.GetOrdinal("JerseyNumber"));
                    player.DateOfBirth = reader.IsDBNull(reader.GetOrdinal("DateOfBirth"))
                        ? (DateTime?)null
                        : reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                }
                ;

                return player;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => GetAnyPlayerById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => GetAnyPlayerById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return player;
            }
        }


        public async Task<PlayerDto> CreatePlayer(PlayerDto playerToCreate)
        {
            PlayerDto newPlayer = new PlayerDto();

            string sqlQuery =
                @"
                    INSERT INTO SoccerAppPlayers (UserId, TeamId, FirstName, LastName, JerseyNumber, DateOfBirth)
                    VALUES (@userId, @teamId, @firstName, @lastName, @jerseyNumber, @dateOfBirth)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                if (!string.IsNullOrWhiteSpace(playerToCreate.UserId.ToString()))
                {
                    command.Parameters.AddWithValue("@userId", playerToCreate.UserId);
                }
                else
                {
                    command.Parameters.AddWithValue("@userId", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(playerToCreate.TeamId.ToString()))
                {
                    command.Parameters.AddWithValue("@teamId", playerToCreate.TeamId);
                }
                else
                {
                    command.Parameters.AddWithValue("@teamId", DBNull.Value);
                }

                command.Parameters.AddWithValue("@firstName", playerToCreate.FirstName);
                command.Parameters.AddWithValue("@lastName", playerToCreate.LastName);

                if (!string.IsNullOrWhiteSpace(playerToCreate.JerseyNumber.ToString()))
                {
                    command.Parameters.AddWithValue("@jerseyNumber", playerToCreate.JerseyNumber);
                }
                else
                {
                    command.Parameters.AddWithValue("@jerseyNumber", DBNull.Value);
                }

                command.Parameters.AddWithValue("@dateOfBirth", playerToCreate.DateOfBirth);

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newPlayerId))
                {
                    newPlayer = await GetPlayerById(newPlayerId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New Plasyer inserted. ID: {newPlayerId} from SoccerAppBackend => PlayersService => CreatePlayer");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new Player record in database from SoccerAppBackend => PlayersService => CreatePlayer");
                }

                return newPlayer;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => CreatePlayer: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newPlayer;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => CreatePlayer: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newPlayer;
            }
        }


        public async Task<PlayerDto> UpdatePlayer(PlayerDto playerToUpdate)
        {
            string sqlQuery =
                @"
                    UPDATE SoccerAppPlayers
                    SET FirstName = @firstName,
                        LastName = @lastName,
                        JerseyNumber = @jerseyNumber,
                        DateOfBirth = @dateOfBirth,
                        ModifiedAt = @modifiedAt
                    WHERE playerId = @playerId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@playerId", playerToUpdate.PlayerId);
                command.Parameters.AddWithValue("@firstName", playerToUpdate.FirstName);
                command.Parameters.AddWithValue("@lastName", playerToUpdate.LastName);
                command.Parameters.AddWithValue("@jerseyNumber", playerToUpdate.JerseyNumber);
                command.Parameters.AddWithValue("@dateOfBirth", playerToUpdate.DateOfBirth);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    playerToUpdate = await GetPlayerById(playerToUpdate.PlayerId);
                }

                return playerToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => UpdatePlayer: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return playerToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => UpdatePlayer: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return playerToUpdate;
            }
        }


        public async Task<PlayerDto> DeactivatePlayerById(int playerId)
        {
            PlayerDto playerToDeactivate = new PlayerDto();

            string sqlQuery =
                @"
                    UPDATE SoccerAppPlayers
                    SET IsActive = 0,
                        ModifiedAt = @modifiedAt
                    WHERE playerId = @playerId
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@playerId", playerId);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    playerToDeactivate = await GetAnyPlayerById(playerId);
                }

                return playerToDeactivate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayersService => DeactivatePlayerById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return playerToDeactivate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayersService => DeactivatePlayerById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return playerToDeactivate;
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class GamesService : IGamesService
    {
        private readonly IDatabaseService databaseService;

        public GamesService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<List<GameDto>> GetAllGames()
        {
            List<GameDto> activeGames = new List<GameDto>();
            string sqlQuery = @"SELECT * FROM SoccerAppGames";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    activeGames.Add(new GameDto
                    {
                        GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                        TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                        OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId")),
                        GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate")),
                        GameLocation = reader.IsDBNull(reader.GetOrdinal("GameLocation"))
                            ? (string?)null
                            : reader.GetString(reader.GetOrdinal("GameLocation")),
                        TeamScore = reader.GetInt32(reader.GetOrdinal("TeamScore")),
                        OpponentScore = reader.GetInt32(reader.GetOrdinal("OpponentScore")),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                            ? (string?)null
                            : reader.GetString(reader.GetOrdinal("Notes")),
                        GameStatus = reader.GetString(reader.GetOrdinal("GameStatus"))
                    });
                };

                return activeGames;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesService => GetAllGames: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return activeGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesService => GetAllGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return activeGames;
            }
        }

        public async Task<GameDto> GetGameById(int gameId)
        {
            GameDto game = new GameDto();
            string sqlQuery = @"SELECT * FROM SoccerAppGames WHERE GameId = @gameId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@gameId", gameId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    game.GameId = reader.GetInt32(reader.GetOrdinal("GameId"));
                    game.TeamId = reader.GetInt32(reader.GetOrdinal("TeamId"));
                    game.OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId"));
                    game.GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate"));
                    game.GameLocation = reader.IsDBNull(reader.GetOrdinal("GameLocation"))
                            ? (string?)null
                            : reader.GetString(reader.GetOrdinal("GameLocation"));
                    game.TeamScore = reader.IsDBNull(reader.GetOrdinal("TeamScore"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("TeamScore"));
                    game.OpponentScore = reader.IsDBNull(reader.GetOrdinal("OpponentScore"))
                            ? (int?)null
                            : reader.GetInt32(reader.GetOrdinal("OpponentScore"));
                    game.Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                            ? (string?)null
                            : reader.GetString(reader.GetOrdinal("Notes"));
                    game.GameStatus = reader.GetString(reader.GetOrdinal("GameStatus"));
                }
                ;

                return game;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesService => GetGameById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return game;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesService => GetGameById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return game;
            }
        }

        public async Task<GameDto> CreateGame(GameDto gameToCreate)
        {
            GameDto newGame = new GameDto();

            string sqlQuery =
                @"
                    INSERT INTO SoccerAppGames (TeamId, OpponentId, GameDate, GameLocation, TeamScore, OpponentScore, Notes)
                    VALUES (@teamId, @opponentId, @gameDate, @gameLocation, @teamScore, @opponentScore, @notes)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@teamId", gameToCreate.TeamId);
                command.Parameters.AddWithValue("@opponentId", gameToCreate.OpponentId);
                command.Parameters.AddWithValue("@gameDate", gameToCreate.GameDate);

                if (!string.IsNullOrWhiteSpace(gameToCreate.GameLocation?.ToString()))
                {
                    command.Parameters.AddWithValue("@gameLocation", gameToCreate.GameLocation);
                }
                else
                {
                    command.Parameters.AddWithValue("@gameLocation", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(gameToCreate.TeamScore?.ToString()))
                {
                    command.Parameters.AddWithValue("@teamScore", gameToCreate.TeamScore);
                }
                else
                {
                    command.Parameters.AddWithValue("@teamScore", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(gameToCreate.OpponentScore?.ToString()))
                {
                    command.Parameters.AddWithValue("@opponentScore", gameToCreate.OpponentScore);
                }
                else
                {
                    command.Parameters.AddWithValue("@opponentScore", DBNull.Value);
                }

                if (!string.IsNullOrWhiteSpace(gameToCreate.Notes?.ToString()))
                {
                    command.Parameters.AddWithValue("@notes", gameToCreate.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@notes", DBNull.Value);
                }

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newGameId))
                {
                    newGame = await GetGameById(newGameId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New Game inserted. ID: {newGameId} from SoccerAppBackend => GamesService => CreateGame");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new Game record in database from SoccerAppBackend => GamesService => CreateGame");
                }

                return newGame;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesService => CreateGame: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newGame;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesService => CreateGame: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newGame;
            }
        }

        public async Task<GameDto> UpdateGame(GameDto gameToUpdate)
        {
            string sqlQuery =
                @"
                    UPDATE SoccerAppGames
                    SET TeamId = @teamId,
                        OpponentId = @opponentId,
                        GameDate = @gameDate,
                        GameLocation = @gameLocation,
                        TeamScore = @teamScore,
                        OpponentScore = @opponentScore,
                        Notes = @notes,
                        GameStatus = @gameStatus,
                        ModifiedAt = @modifiedAt
                    WHERE GameId = @gameId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@gameId", gameToUpdate.GameId);
                command.Parameters.AddWithValue("@teamId", gameToUpdate.TeamId);
                command.Parameters.AddWithValue("@opponentId", gameToUpdate.OpponentId);
                command.Parameters.AddWithValue("@gameDate", gameToUpdate.GameDate);
                command.Parameters.AddWithValue("@gameLocation", gameToUpdate.GameLocation);
                command.Parameters.AddWithValue("@teamScore", gameToUpdate.TeamScore);
                command.Parameters.AddWithValue("@opponentScore", gameToUpdate.OpponentScore);
                if (!string.IsNullOrWhiteSpace(gameToUpdate.Notes?.ToString()))
                {
                    command.Parameters.AddWithValue("@notes", gameToUpdate.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@notes", DBNull.Value);
                }
                command.Parameters.AddWithValue("@gameStatus", gameToUpdate.GameStatus);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    gameToUpdate = await GetGameById(gameToUpdate.GameId);
                }

                return gameToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesService => UpdateGame: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return gameToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesService => UpdateGame: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return gameToUpdate;
            }
        }
    }
}

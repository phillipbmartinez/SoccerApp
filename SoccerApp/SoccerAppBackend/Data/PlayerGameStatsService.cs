using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class PlayerGameStatsService : IPlayerGameStatsService
    {
        private readonly IDatabaseService databaseService;

        public PlayerGameStatsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<List<PlayerGameStatDto>> GetAllPlayerGameStats()
        {
            List<PlayerGameStatDto> playerGameStats = new List<PlayerGameStatDto>();
            string sqlQuery = @"SELECT * FROM SoccerAppPlayerGameStats";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    playerGameStats.Add(new PlayerGameStatDto
                    {
                        PlayerGameStatId = reader.GetInt32(reader.GetOrdinal("PlayerGameStatId")),
                        GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                        PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId")),
                        MinutesPlayed = reader.GetInt32(reader.GetOrdinal("MinutesPlayed")),
                        Goals = reader.GetInt32(reader.GetOrdinal("Goals")),
                        Assists = reader.GetInt32(reader.GetOrdinal("Assists")),
                        Shots = reader.GetInt32(reader.GetOrdinal("Shots")),
                        ShotsOnTarget = reader.GetInt32(reader.GetOrdinal("ShotsOnTarget")),
                        PassesCompleted = reader.GetInt32(reader.GetOrdinal("PassesCompleted")),
                        Tackles = reader.GetInt32(reader.GetOrdinal("Tackles")),
                        Interceptions = reader.GetInt32(reader.GetOrdinal("Interceptions")),
                        Saves = reader.GetInt32(reader.GetOrdinal("Saves")),
                        YellowCards = reader.GetInt32(reader.GetOrdinal("YellowCards")),
                        RedCards = reader.GetInt32(reader.GetOrdinal("RedCards")),
                        Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                            ? (string?)null
                            : reader.GetString(reader.GetOrdinal("Notes")),
                    });
                };

                return playerGameStats;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => GetAllPlayerGameStats: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return playerGameStats;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => GetAllPlayerGameStats: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return playerGameStats;
            }
        }

        public async Task<PlayerGameStatDto> GetPlayerGameStatById(int playerGameStatId)
        {
            PlayerGameStatDto playerGameStat = new PlayerGameStatDto();
            string sqlQuery = @"SELECT * FROM SoccerAppPlayerGameStats WHERE PlayerGameStatId = @playerGameStatId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@playerGameStatId", playerGameStatId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    playerGameStat.PlayerGameStatId = reader.GetInt32(reader.GetOrdinal("PlayerGameStatId"));
                    playerGameStat.GameId = reader.GetInt32(reader.GetOrdinal("GameId"));
                    playerGameStat.PlayerId = reader.GetInt32(reader.GetOrdinal("PlayerId"));
                    playerGameStat.MinutesPlayed = reader.GetInt32(reader.GetOrdinal("MinutesPlayed"));
                    playerGameStat.Goals = reader.GetInt32(reader.GetOrdinal("Goals"));
                    playerGameStat.Assists = reader.GetInt32(reader.GetOrdinal("Assists"));
                    playerGameStat.Shots = reader.GetInt32(reader.GetOrdinal("Shots"));
                    playerGameStat.ShotsOnTarget = reader.GetInt32(reader.GetOrdinal("ShotsOnTarget"));
                    playerGameStat.PassesCompleted = reader.GetInt32(reader.GetOrdinal("PassesCompleted"));
                    playerGameStat.Tackles = reader.GetInt32(reader.GetOrdinal("Tackles"));
                    playerGameStat.Interceptions = reader.GetInt32(reader.GetOrdinal("Interceptions"));
                    playerGameStat.Saves = reader.GetInt32(reader.GetOrdinal("Saves"));
                    playerGameStat.YellowCards = reader.GetInt32(reader.GetOrdinal("YellowCards"));
                    playerGameStat.RedCards = reader.GetInt32(reader.GetOrdinal("RedCards"));
                    playerGameStat.Notes = reader.IsDBNull(reader.GetOrdinal("Notes"))
                        ? (string?)null
                        : reader.GetString(reader.GetOrdinal("Notes"));
                };

                return playerGameStat;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => GetPlayerGameStatById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return playerGameStat;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => GetPlayerGameStatById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return playerGameStat;
            }
        }

        public async Task<PlayerGameStatDto> CreatePlayerGameStat(PlayerGameStatDto playerGameStatToCreate)
        {
            PlayerGameStatDto newPlayerGameStat = new PlayerGameStatDto();

            string sqlQuery =
                @"
                    INSERT INTO SoccerAppPlayerGameStats (GameId, PlayerId, MinutesPlayed, Goals, Assists, Shots, ShotsOnTarget, PassesCompleted, Tackles, Interceptions, Saves, YellowCards, RedCards, Notes)
                    VALUES (@gameId, @playerId, @minutesPlayed, @goals, @assists, @shots, @shotsOnTarget, @passesCompleted, @tackles, @interceptions, @saves, @yellowCards, @redCards, @notes)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@gameId", playerGameStatToCreate.GameId);
                command.Parameters.AddWithValue("@playerId", playerGameStatToCreate.PlayerId);

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.MinutesPlayed.ToString()))
                {
                    command.Parameters.AddWithValue("@minutesPlayed", playerGameStatToCreate.MinutesPlayed);
                }
                else
                {
                    command.Parameters.AddWithValue("@minutesPlayed", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Goals.ToString()))
                {
                    command.Parameters.AddWithValue("@goals", playerGameStatToCreate.Goals);
                }
                else
                {
                    command.Parameters.AddWithValue("@goals", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Assists.ToString()))
                {
                    command.Parameters.AddWithValue("@assists", playerGameStatToCreate.Assists);
                }
                else
                {
                    command.Parameters.AddWithValue("@assists", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Shots.ToString()))
                {
                    command.Parameters.AddWithValue("@shots", playerGameStatToCreate.Shots);
                }
                else
                {
                    command.Parameters.AddWithValue("@shots", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.ShotsOnTarget.ToString()))
                {
                    command.Parameters.AddWithValue("@shotsOnTarget", playerGameStatToCreate.ShotsOnTarget);
                }
                else
                {
                    command.Parameters.AddWithValue("@shotsOnTarget", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.PassesCompleted.ToString()))
                {
                    command.Parameters.AddWithValue("@passesCompleted", playerGameStatToCreate.PassesCompleted);
                }
                else
                {
                    command.Parameters.AddWithValue("@passesCompleted", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Tackles.ToString()))
                {
                    command.Parameters.AddWithValue("@tackles", playerGameStatToCreate.Tackles);
                }
                else
                {
                    command.Parameters.AddWithValue("@tackles", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Interceptions.ToString()))
                {
                    command.Parameters.AddWithValue("@interceptions", playerGameStatToCreate.Interceptions);
                }
                else
                {
                    command.Parameters.AddWithValue("@interceptions", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Saves.ToString()))
                {
                    command.Parameters.AddWithValue("@saves", playerGameStatToCreate.Saves);
                }
                else
                {
                    command.Parameters.AddWithValue("@saves", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.YellowCards.ToString()))
                {
                    command.Parameters.AddWithValue("@yellowCards", playerGameStatToCreate.YellowCards);
                }
                else
                {
                    command.Parameters.AddWithValue("@yellowCards", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.RedCards.ToString()))
                {
                    command.Parameters.AddWithValue("@redCards", playerGameStatToCreate.RedCards);
                }
                else
                {
                    command.Parameters.AddWithValue("@redCards", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToCreate.Notes?.ToString()))
                {
                    command.Parameters.AddWithValue("@notes", playerGameStatToCreate.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@notes", DBNull.Value);
                }

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newPlayerGameStatId))
                {
                    newPlayerGameStat = await GetPlayerGameStatById(newPlayerGameStatId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New PlayerGameStat inserted. ID: {newPlayerGameStatId} from SoccerAppBackend => PlayerGameStatsService => CreatePlayerGameStat");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new PlayerGameStat record in database from SoccerAppBackend => PlayerGameStatsService => CreatePlayerGameStat");
                }

                return newPlayerGameStat;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => CreatePlayerGameStat: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newPlayerGameStat;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => CreatePlayerGameStat: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newPlayerGameStat;
            }
        }

        public async Task<PlayerGameStatDto> UpdatePlayerGameStat(PlayerGameStatDto playerGameStatToUpdate)
        {
            string sqlQuery =
                @"
                    UPDATE SoccerAppPlayerGameStats
                    SET GameId = @gameId,
                        PlayerId = @playerId,
                        MinutesPlayed = @minutesPlayed,
                        Goals = @goals,
                        Assists = @assists,
                        Shots = @shots,
                        ShotsOnTarget = @shotsOnTarget,
                        PassesCompleted = @passesCompleted,
                        Tackles = @tackles,
                        Interceptions = @interceptions,
                        Saves = @saves,
                        YellowCards = @yellowCards,
                        RedCards = @redCards,
                        Notes = @notes,
                        ModifiedAt = @modifiedAt
                    WHERE PlayerGameStatId = @playerGameStatId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@playerGameStatId", playerGameStatToUpdate.PlayerGameStatId);
                command.Parameters.AddWithValue("@gameId", playerGameStatToUpdate.GameId);
                command.Parameters.AddWithValue("@playerId", playerGameStatToUpdate.PlayerId);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.MinutesPlayed.ToString()))
                {
                    command.Parameters.AddWithValue("@minutesPlayed", playerGameStatToUpdate.MinutesPlayed);
                }
                else
                {
                    command.Parameters.AddWithValue("@minutesPlayed", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Goals.ToString()))
                {
                    command.Parameters.AddWithValue("@goals", playerGameStatToUpdate.Goals);
                }
                else
                {
                    command.Parameters.AddWithValue("@goals", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Assists.ToString()))
                {
                    command.Parameters.AddWithValue("@assists", playerGameStatToUpdate.Assists);
                }
                else
                {
                    command.Parameters.AddWithValue("@assists", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Shots.ToString()))
                {
                    command.Parameters.AddWithValue("@shots", playerGameStatToUpdate.Shots);
                }
                else
                {
                    command.Parameters.AddWithValue("@shots", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.ShotsOnTarget.ToString()))
                {
                    command.Parameters.AddWithValue("@shotsOnTarget", playerGameStatToUpdate.ShotsOnTarget);
                }
                else
                {
                    command.Parameters.AddWithValue("@shotsOnTarget", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.PassesCompleted.ToString()))
                {
                    command.Parameters.AddWithValue("@passesCompleted", playerGameStatToUpdate.PassesCompleted);
                }
                else
                {
                    command.Parameters.AddWithValue("@passesCompleted", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Tackles.ToString()))
                {
                    command.Parameters.AddWithValue("@tackles", playerGameStatToUpdate.Tackles);
                }
                else
                {
                    command.Parameters.AddWithValue("@tackles", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Interceptions.ToString()))
                {
                    command.Parameters.AddWithValue("@interceptions", playerGameStatToUpdate.Interceptions);
                }
                else
                {
                    command.Parameters.AddWithValue("@interceptions", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Saves.ToString()))
                {
                    command.Parameters.AddWithValue("@saves", playerGameStatToUpdate.Saves);
                }
                else
                {
                    command.Parameters.AddWithValue("@saves", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.YellowCards.ToString()))
                {
                    command.Parameters.AddWithValue("@yellowCards", playerGameStatToUpdate.YellowCards);
                }
                else
                {
                    command.Parameters.AddWithValue("@yellowCards", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.RedCards.ToString()))
                {
                    command.Parameters.AddWithValue("@redCards", playerGameStatToUpdate.RedCards);
                }
                else
                {
                    command.Parameters.AddWithValue("@redCards", 0);
                }

                if (!string.IsNullOrWhiteSpace(playerGameStatToUpdate.Notes?.ToString()))
                {
                    command.Parameters.AddWithValue("@notes", playerGameStatToUpdate.Notes);
                }
                else
                {
                    command.Parameters.AddWithValue("@notes", DBNull.Value);
                }

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    playerGameStatToUpdate = await GetPlayerGameStatById(playerGameStatToUpdate.PlayerGameStatId);
                }

                return playerGameStatToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => UpdatePlayerGameStat: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return playerGameStatToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => PlayerGameStatsService => UpdatePlayerGameStat: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return playerGameStatToUpdate;
            }
        }
    }
}

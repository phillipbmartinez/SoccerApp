using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class GamesTeamsOpponentsService : IGamesTeamsOpponentsService
    {
        private readonly IDatabaseService databaseService;

        public GamesTeamsOpponentsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<List<GameTeamOpponentDto>> GetGames()
        {
            List<GameTeamOpponentDto> games = new List<GameTeamOpponentDto>();

            string sqlQuery = "SELECT * FROM [vw_SoccerAppGamesTeamsOpponents]";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    games.Add(new GameTeamOpponentDto
                    {
                        GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                        GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate")),
                        GameLocation = reader.IsDBNull(reader.GetOrdinal("GameLocation"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("GameLocation")),
                        GameStatus = reader.GetString(reader.GetOrdinal("GameStatus")),
                        AgeGroup = reader.IsDBNull(reader.GetOrdinal("AgeGroup"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("AgeGroup")),
                        TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                        TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                        TeamScore = reader.IsDBNull(reader.GetOrdinal("TeamScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("TeamScore")),
                        OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId")),
                        OpponentName = reader.GetString(reader.GetOrdinal("OpponentName")),
                        OpponentScore = reader.IsDBNull(reader.GetOrdinal("OpponentScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("OpponentScore")),
                    });
                };

                return games;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetGames: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return games;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return games;
            }
        }

        public async Task<List<GameTeamOpponentDto>> GetUpcomingGames()
        {
            List<GameTeamOpponentDto> upcomingGames = new List<GameTeamOpponentDto>();
            string today = DateTime.Now.ToString("yyyy/MM/dd");

            Console.WriteLine(today);

            string sqlQuery = "SELECT * FROM [vw_SoccerAppGamesTeamsOpponents] WHERE GameDate > @today";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@today", today);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    upcomingGames.Add(new GameTeamOpponentDto
                    {
                        GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                        GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate")),
                        GameLocation = reader.IsDBNull(reader.GetOrdinal("GameLocation"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("GameLocation")),
                        GameStatus = reader.GetString(reader.GetOrdinal("GameStatus")),
                        AgeGroup = reader.IsDBNull(reader.GetOrdinal("AgeGroup"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("AgeGroup")),
                        TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                        TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                        TeamScore = reader.IsDBNull(reader.GetOrdinal("TeamScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("TeamScore")),
                        OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId")),
                        OpponentName = reader.GetString(reader.GetOrdinal("OpponentName")),
                        OpponentScore = reader.IsDBNull(reader.GetOrdinal("OpponentScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("OpponentScore")),
                    });
                }
                ;

                return upcomingGames;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetUpcomingGames: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return upcomingGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetUpcomingGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return upcomingGames;
            }
        }

        public async Task<List<GameTeamOpponentDto>> GetPreviousGames()
        {
            List<GameTeamOpponentDto> upcomingGames = new List<GameTeamOpponentDto>();
            string today = DateTime.Now.ToString("yyyy/MM/dd");

            Console.WriteLine(today);

            string sqlQuery = "SELECT * FROM [vw_SoccerAppGamesTeamsOpponents] WHERE GameDate < @today";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@today", today);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    upcomingGames.Add(new GameTeamOpponentDto
                    {
                        GameId = reader.GetInt32(reader.GetOrdinal("GameId")),
                        GameDate = reader.GetDateTime(reader.GetOrdinal("GameDate")),
                        GameLocation = reader.IsDBNull(reader.GetOrdinal("GameLocation"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("GameLocation")),
                        GameStatus = reader.GetString(reader.GetOrdinal("GameStatus")),
                        AgeGroup = reader.IsDBNull(reader.GetOrdinal("AgeGroup"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("AgeGroup")),
                        TeamId = reader.GetInt32(reader.GetOrdinal("TeamId")),
                        TeamName = reader.GetString(reader.GetOrdinal("TeamName")),
                        TeamScore = reader.IsDBNull(reader.GetOrdinal("TeamScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("TeamScore")),
                        OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId")),
                        OpponentName = reader.GetString(reader.GetOrdinal("OpponentName")),
                        OpponentScore = reader.IsDBNull(reader.GetOrdinal("OpponentScore"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("OpponentScore")),
                    });
                }
                ;

                return upcomingGames;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetPreviousGames: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return upcomingGames;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => GamesTeamsOpponentsService => GetPreviousGames: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return upcomingGames;
            }
        }
    }
}

using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class OpponentsService : IOpponentsService
    {
        private readonly IDatabaseService databaseService;

        public OpponentsService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }

        public async Task<List<OpponentDto>> GetActiveOpponents()
        {
            List<OpponentDto> activeOpponents = new List<OpponentDto>();
            string sqlQuery = @"SELECT * FROM SoccerAppOpponents";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    activeOpponents.Add(new OpponentDto
                    {
                        OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId")),
                        OpponentName = reader.GetString(reader.GetOrdinal("OpponentName")),
                        AgeGroup = reader.GetString(reader.GetOrdinal("AgeGroup"))
                    });
                };

                return activeOpponents;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => OpponentsService => GetActiveOpponents: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return activeOpponents;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => OpponentsService => GetActiveOpponents: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return activeOpponents;
            }
        }

        public async Task<OpponentDto> GetOpponentById(int opponentId)
        {
            OpponentDto opponent = new OpponentDto();
            string sqlQuery = @"SELECT * FROM SoccerAppOpponents WHERE OpponentId = @opponentId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@opponentId", opponentId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    opponent.OpponentId = reader.GetInt32(reader.GetOrdinal("OpponentId"));
                    opponent.OpponentName = reader.GetString(reader.GetOrdinal("OpponentName"));
                    opponent.AgeGroup = reader.GetString(reader.GetOrdinal("AgeGroup"));
                };

                return opponent;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => OpponentsService => GetOpponentById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return opponent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => OpponentsService => GetOpponentById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return opponent;
            }
        }

        public async Task<OpponentDto> CreateOpponent(OpponentDto opponenToCreate)
        {
            OpponentDto newOpponent = new OpponentDto();

            string sqlQuery =
                @"
                    INSERT INTO SoccerAppOpponents (OpponentName, AgeGroup)
                    VALUES (@opponentName, @ageGroup)
                    SELECT CAST(SCOPE_IDENTITY() AS INT);
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@opponentName", opponenToCreate.OpponentName);

                if (!string.IsNullOrWhiteSpace(opponenToCreate.AgeGroup?.ToString()))
                {
                    command.Parameters.AddWithValue("@ageGroup", opponenToCreate.AgeGroup);
                }
                else
                {
                    command.Parameters.AddWithValue("@ageGroup", DBNull.Value);
                }

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newOpponentId))
                {
                    newOpponent = await GetOpponentById(newOpponentId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New Opponent inserted. ID: {newOpponentId} from SoccerAppBackend => OpponentsService => CreateOpponent");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new Opponent record in database from SoccerAppBackend => OpponentsService => CreateOpponent");
                }

                return newOpponent;

            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => OpponentsService => CreateOpponent: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newOpponent;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => OpponentsService => CreateOpponent: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newOpponent;
            }
        }

        public async Task<OpponentDto> UpdateOpponent(OpponentDto opponentToUpdate)
        {
            string sqlQuery =
                @"
                    UPDATE SoccerAppOpponents
                    SET OpponentName = @opponentName,
                        AgeGroup = @ageGroup,
                        ModifiedAt = @modifiedAt
                    WHERE OpponentId = @opponentId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@opponentId", opponentToUpdate.OpponentId);
                command.Parameters.AddWithValue("@opponentName", opponentToUpdate.OpponentName);
                command.Parameters.AddWithValue("@AgeGroup", opponentToUpdate.AgeGroup);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    opponentToUpdate = await GetOpponentById(opponentToUpdate.OpponentId);
                }

                return opponentToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => OpponentsService => UpdateOpponent: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return opponentToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => OpponentsService => UpdateOpponent: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return opponentToUpdate;
            }
        }

        public async Task<OpponentDto> DeactivateOpponent(int opponentId)
        {
            OpponentDto opponentToDeactivate = new OpponentDto();

            string sqlQuery =
                @"
                    UPDATE SoccerAppOpponents
                    SET IsActive = 0,
                        ModifiedAt = @modifiedAt
                    WHERE OpponentId = @opponentId
                ";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@opponentId", opponentId);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    opponentToDeactivate = await GetOpponentById(opponentId);
                }

                return opponentToDeactivate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => OpponentsService => DeactivateOpponent: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return opponentToDeactivate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => OpponentsService => DeactivateOpponent: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return opponentToDeactivate;
            }
        }
    }
}

using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Win32.SafeHandles;
using SoccerAppBackend.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace SoccerAppBackend.Data
{
    public class CoachesService : ICoachesService
    {
        private readonly IDatabaseService databaseSerivce;

        public CoachesService(IDatabaseService databaseSerivce)
        {
            this.databaseSerivce = databaseSerivce;
        }

        public async Task<List<Coach>> ReturnAllActiveCoaches()
        {
            List<Coach> coaches = new List<Coach>();

            string sqlQuery = "SELECT * FROM SoccerAppCoaches WHERE IsActive = 1";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coaches.Add(new Coach
                    {
                        CoachId = reader.GetInt32(reader.GetOrdinal("CoachId")),
                        CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("CoachingLicense")),
                        StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("UserId")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                    });
                };

                return coaches;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllCoaches: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coaches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coaches;
            }
        }

        public async Task<List<Coach>> ReturnAllInactiveCoaches()
        {
            List<Coach> coaches = new List<Coach>();

            string sqlQuery = "SELECT * FROM SoccerAppCoaches WHERE IsActive = 0";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coaches.Add(new Coach
                    {
                        CoachId = reader.GetInt32(reader.GetOrdinal("CoachId")),
                        CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("CoachingLicense")),
                        StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                        ? null
                        : reader.GetInt32(reader.GetOrdinal("UserId")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                    });
                }
                ;

                return coaches;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllInactiveCoaches: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coaches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnAllInactiveCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coaches;
            }
        }


        public async Task<Coach> ReturnCoachById(int coachId)
        {
            Coach coach = new Coach();

            string sqlQuery = "SELECT * FROM SoccerAppCoaches WHERE CoachId = @coachId AND IsActive = 1";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@coachId", coachId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coach.CoachId = reader.GetInt32(reader.GetOrdinal("CoachId"));
                    coach.CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CoachingLicense"));
                    coach.StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate"));
                    coach.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                    coach.UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("UserId"));
                    coach.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    coach.ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("ModifiedAt"));
                }

                return coach;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnCoachById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coach;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnCoachById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coach;
            }
        }


        public async Task<Coach> ReturnAnyCoachById(int coachId)
        {
            Coach coach = new Coach();

            string sqlQuery = "SELECT * FROM SoccerAppCoaches WHERE CoachId = @coachId";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@coachId", coachId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    coach.CoachId = reader.GetInt32(reader.GetOrdinal("CoachId"));
                    coach.CoachingLicense = reader.IsDBNull(reader.GetOrdinal("CoachingLicense"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("CoachingLicense"));
                    coach.StartedCoachingDate = reader.GetDateTime(reader.GetOrdinal("StartedCoachingDate"));
                    coach.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                    coach.UserId = reader.IsDBNull(reader.GetOrdinal("UserId"))
                    ? null
                    : reader.GetInt32(reader.GetOrdinal("UserId"));
                    coach.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    coach.ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("ModifiedAt"));
                }

                return coach;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnCoachById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coach;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => ReturnCoachById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coach;
            }
        }


        public async Task<Coach> DeleteCoachById(int coachId)
        {
            Coach coachToDelete = new Coach();

            string sqlQuery = "UPDATE SoccerAppCoaches SET IsActive = 0 WHERE CoachId = @coachId";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@coachId", coachId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    coachToDelete = await ReturnAnyCoachById(coachId);
                }

                return coachToDelete;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => DeleteCoachById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coachToDelete;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => DeleteCoachById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coachToDelete;
            }
        }


        public async Task<Coach> UpdateCoach(Coach coachToUpdate)
        {
            string sqlQuery = 
                @"  UPDATE SoccerAppCoaches
                    SET CoachingLicense = @coachingLicense, StartedCoachingDate = @startedCoachingDate, ModifiedAt = @modifiedAt
                    WHERE CoachId = @coachId";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@coachingLicense", coachToUpdate.CoachingLicense);
                command.Parameters.AddWithValue("@startedCoachingDate", coachToUpdate.StartedCoachingDate);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);
                command.Parameters.AddWithValue("@coachId", coachToUpdate.CoachId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    coachToUpdate = await ReturnAnyCoachById(coachToUpdate.CoachId);
                }

                return coachToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => UpdateCoachById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coachToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => UpdateCoachById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coachToUpdate;
            }
        }
    }
}

using System.ComponentModel;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
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

        public async Task<List<Coach>> ReturnAllCoaches()
        {
            List<Coach> coaches = new List<Coach>();

            string sqlQuery = "SELECT * FROM SoccerAppCoaches";

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
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => CoachesService => GetAllCoaches: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return coaches;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => CoachesService => GetAllCoaches: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return coaches;
            }
        }
    }
}

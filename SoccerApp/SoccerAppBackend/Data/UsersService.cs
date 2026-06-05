using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDatabaseService databaseSerivce;

        public UsersService(IDatabaseService databaseSerivce)
        {
            this.databaseSerivce = databaseSerivce;
        }


        public async Task<List<User>> GetActiveUsers()
        {
            List<User> activeUsers = new List<User>();

            string sqlQuery = "SELECT * FROM SoccerAppUsers WHERE IsActive = 1";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    activeUsers.Add(new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                    });
                };

                return activeUsers;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => GetActiveUsers: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return activeUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => GetActiveUsers: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return activeUsers;
            }
        }


        public async Task<List<User>> GetInactiveUsers()
        {
            List<User> inactiveUsers = new List<User>();

            string sqlQuery = "SELECT * FROM SoccerAppUsers WHERE IsActive = 0";

            try
            {
                using SqlConnection connection = databaseSerivce.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    inactiveUsers.Add(new User
                    {
                        UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                        FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                        LastName = reader.GetString(reader.GetOrdinal("LastName")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                        DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                        ? null
                        : reader.GetDateTime(reader.GetOrdinal("ModifiedAt")),
                    });
                };

                return inactiveUsers;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => GetInactiveUsers: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return inactiveUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => GetInactiveUsers: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return inactiveUsers;
            }
        }
    }
}

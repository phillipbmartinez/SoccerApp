using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class UsersService : IUsersService
    {
        private readonly IDatabaseService databaseService;

        public UsersService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }


        public async Task<List<User>> GetActiveUsers()
        {
            List<User> activeUsers = new List<User>();

            string sqlQuery = "SELECT * FROM SoccerAppUsers WHERE IsActive = 1";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
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
                using SqlConnection connection = databaseService.CreateDbConnection();
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


        public async Task<User> GetUserById(int userId)
        {
            User user = new User();

            string sqlQuery = "SELECT * FROM SoccerAppUsers WHERE UserId = @userId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    user.UserId = reader.GetInt32(reader.GetOrdinal("UserId"));
                    user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                    user.Email = reader.GetString(reader.GetOrdinal("Email"));
                    user.DateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));
                    user.IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                    user.RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"));
                    user.CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"));
                    user.ModifiedAt = reader.IsDBNull(reader.GetOrdinal("ModifiedAt"))
                    ? null
                    : reader.GetDateTime(reader.GetOrdinal("ModifiedAt"));
                }

                return user;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => GetUserById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => GetUserById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return user;
            }
        }


        public async Task<User> DeactivateUserById(int userId)
        {
            User userToDeactivate = new User();

            string sqlQuery = "UPDATE SoccerAppUsers SET IsActive = 0 WHERE UserId = @userId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    userToDeactivate = await GetUserById(userId);
                }

                return userToDeactivate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => DeactivateUserById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return userToDeactivate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => DeactivateUserById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return userToDeactivate;
            }
        }


        public async Task<User> UpdateUser(User userToUpdate)
        {
            string sqlQuery =
                @"  UPDATE SoccerAppUsers
                    SET FirstName = @firstName, Lastname = @lastName, Email = @email, DateOfBirth = @dateOfBirth, RoleId = @roleId, ModifiedAt = @modifiedAt
                    WHERE userId = @userId
                    SELECT CAST(SCOPE_IDENTITY() AS INT)";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@userId", userToUpdate.UserId);
                command.Parameters.AddWithValue("@firstName", userToUpdate.FirstName);
                command.Parameters.AddWithValue("@lastName", userToUpdate.LastName);
                command.Parameters.AddWithValue("@email", userToUpdate.Email);
                command.Parameters.AddWithValue("@dateOfBirth", userToUpdate.DateOfBirth);
                command.Parameters.AddWithValue("@roleId", userToUpdate.RoleId);
                command.Parameters.AddWithValue("@modifiedAt", DateTime.Now);

                int rowsAffected = await command.ExecuteNonQueryAsync();

                if (rowsAffected > 0)
                {
                    userToUpdate = await GetUserById(userToUpdate.UserId);
                }

                return userToUpdate;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => UpdateUser: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return userToUpdate;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => UpdateUser: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return userToUpdate;
            }
        }


        public async Task<User> CreateUser(User userToCreate)
        {
            User newUser = new User();

            string sqlQuery = @"
                INSERT INTO SoccerAppUsers (FirstName, LastName, Email, DateOfBirth, RoleId)
                VALUES (@firstName, @lastName, @email, @dateOfBirth, @roleId)
                SELECT CAST(SCOPE_IDENTITY() AS INT);";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@firstName", userToCreate.FirstName);
                command.Parameters.AddWithValue("@lastName", userToCreate.LastName);
                command.Parameters.AddWithValue("@email", userToCreate.Email);
                command.Parameters.AddWithValue("@dateOfBirth", userToCreate.DateOfBirth);

                if (!string.IsNullOrWhiteSpace(userToCreate.RoleId.ToString()))
                {
                    command.Parameters.AddWithValue("@roleId", userToCreate.RoleId);
                }
                else
                {
                    command.Parameters.AddWithValue("@roleId", DBNull.Value);
                }

                var result = await command.ExecuteScalarAsync();

                if (result != null && int.TryParse(result.ToString(), out int newUserId))
                {
                    newUser = await GetUserById(newUserId);

                    Console.WriteLine($"[SUCCESS : {DateTime.Now}] - New User inserted. ID: {newUserId} from SoccerAppBackend => UsersService => CreateUser");
                }
                else
                {
                    Console.WriteLine($"[ERROR : {DateTime.Now}] - Error creating new User record in database from SoccerAppBackend => UsersService => CreateUser");
                }

                return newUser;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => UsersService => CreateUser: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return newUser;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => UsersService => CreateUser: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return newUser;
            }
        }
    }
}

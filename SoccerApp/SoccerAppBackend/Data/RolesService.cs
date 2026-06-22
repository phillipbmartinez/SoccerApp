using Microsoft.Data.SqlClient;
using SoccerAppBackend.Models;

namespace SoccerAppBackend.Data
{
    public class RolesService : IRolesService
    {
        private readonly IDatabaseService databaseService;

        public RolesService(IDatabaseService databaseService)
        {
            this.databaseService = databaseService;
        }


        public async Task<List<RoleDto>> GetRoles()
        {
            List<RoleDto> roles = new List<RoleDto>();

            string sqlQuery = "SELECT * FROM SoccerAppRoles";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    roles.Add(new RoleDto
                    {
                        RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                        RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                    });
                }
                ;

                return roles;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => RolesService => GetRoles: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return roles;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => RolesService => GetRoles: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return roles;
            }
        }

        public async Task<RoleDto> GetRoleById(int roleId)
        {
            RoleDto role = new RoleDto();

            string sqlQuery = "SELECT * FROM SoccerAppRoles WHERE RoleId = @roleId";

            try
            {
                using SqlConnection connection = databaseService.CreateDbConnection();
                await connection.OpenAsync();
                using SqlCommand command = new SqlCommand(sqlQuery, connection);

                command.Parameters.AddWithValue("@roleId", roleId);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {

                    role.RoleId = reader.GetInt32(reader.GetOrdinal("RoleId"));
                    role.RoleName = reader.GetString(reader.GetOrdinal("RoleName"));
                };

                return role;
            }
            catch (SqlException sqlEx)
            {
                Console.WriteLine($"[SQL EXCEPTION thrown from SoccerAppBackend => RolesService => GetRoleById: {DateTime.Now}] - SQL Exception: {sqlEx.Message}");
                Console.WriteLine(sqlEx.StackTrace);
                return role;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EXCEPTION thrown from SoccerAppBackend => RolesService => GetRoleById: {DateTime.Now}] - Exception: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return role;
            }
        }
    }
}

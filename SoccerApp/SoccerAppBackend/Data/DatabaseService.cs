using Microsoft.Data.SqlClient;

namespace SoccerAppBackend.Data
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration configuration;

        public DatabaseService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public SqlConnection CreateDbConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("CSharpSqlDbConnection"));
        }

        public SqlConnection CreateLogFilesDbConnection()
        {
            return new SqlConnection(configuration.GetConnectionString("LogFilesSqlDbConnection"));
        }
    }
}

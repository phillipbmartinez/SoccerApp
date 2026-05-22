using Microsoft.Data.SqlClient;

namespace SoccerAppBackend.Data
{
    public class Database : IDatabase
    {
        private readonly IConfiguration configuration;

        public Database(IConfiguration configuration)
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

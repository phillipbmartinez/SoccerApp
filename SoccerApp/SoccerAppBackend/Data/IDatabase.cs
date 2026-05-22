using Microsoft.Data.SqlClient;

namespace SoccerAppBackend.Data
{
    public interface IDatabase
    {
        SqlConnection CreateDbConnection();
        SqlConnection CreateLogFilesDbConnection();
    }
}
using Microsoft.Data.SqlClient;

namespace SoccerAppBackend.Data
{
    public interface IDatabaseService
    {
        SqlConnection CreateDbConnection();
        SqlConnection CreateLogFilesDbConnection();
    }
}
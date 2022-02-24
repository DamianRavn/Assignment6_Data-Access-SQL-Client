using Microsoft.Data.SqlClient;

namespace ProjectB.Repositories
{
    public class ConnectionStringHelper
    {
        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = @"DESKTOP-DLED5R6\SQLEXPRESS";
            connectionStringBuilder.InitialCatalog = "Chinook";
            connectionStringBuilder.IntegratedSecurity = true;
            connectionStringBuilder.TrustServerCertificate = true;
            return connectionStringBuilder.ConnectionString;
        }
    }
}

using System.Data;
using Microsoft.Data.SqlClient;

namespace UserDemo.Repository
{
    public class DapperContext : IDapperContext
    {
        string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=UserDB; Trusted_Connection=True; MultipleActiveResultSets=true";

        public IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = new SqlConnection(connectionString);

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }
    }
}

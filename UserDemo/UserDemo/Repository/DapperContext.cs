using System.Data;
using Microsoft.Data.SqlClient;

namespace UserDemo.Repository
{
    public class DapperContext : IDapperContext
    {
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=(localdb)\\MSSQLLocalDB; Database=UserDB; Trusted_Connection=True; MultipleActiveResultSets=true";
        }
        public IDbConnection CreateConnection()
        {
            IDbConnection dbConnection = new SqlConnection(_connectionString);

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return dbConnection;
        }
    }
}

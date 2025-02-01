using System.Data;

namespace UserDemo.Repository
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}

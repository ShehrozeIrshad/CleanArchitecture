using Microsoft.Data.SqlClient;

namespace Application.Abstractions
{
    public interface ISqlConnectionFactory
    {
        SqlConnection CreateConnection();
    }
}

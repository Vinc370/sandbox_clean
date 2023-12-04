using MySql.Data.MySqlClient;
using System.Data;

namespace dotnet.Data
{
    public class DapperContext
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        public DapperContext(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetConnectionString("Default");
        }
        public IDbConnection CreateConnection() => new MySqlConnection(connectionString);
    }
}
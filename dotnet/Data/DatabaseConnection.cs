using MySql.Data.MySqlClient;

namespace dotnet.Data
{
    public class DatabaseConnection
    {
        public string ConnectionString { get; set; }

        public DatabaseConnection(string connectionString)
        {
            ConnectionString = connectionString;
        }

        private MySqlConnection getConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}

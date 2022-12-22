using MySql.Data.MySqlClient;
using System.Data;

namespace Data4SalesChallenge.Context
{
    public class DBContext
    {
        private readonly string _connectionString;

        public DBContext()
        {
            _connectionString = $"server={Environment.GetEnvironmentVariable("SERVER")};user={Environment.GetEnvironmentVariable("USER")};database={Environment.GetEnvironmentVariable("DB")};password={Environment.GetEnvironmentVariable("PASSWORD")}";
        }

        public IDbConnection CreateConnection() => new MySqlConnection(_connectionString);
    }
}

using Npgsql;

namespace SoundSoulInfrastructure
{
    public class DataContext
    {
        private static string? ConnectionString {  get; set; }
        public DataContext(string connectionString) { 
            ConnectionString = connectionString;
        }
        public static NpgsqlConnection GetConnectionPostgreSql()
        {
            return new NpgsqlConnection(ConnectionString);
        }
    }
}

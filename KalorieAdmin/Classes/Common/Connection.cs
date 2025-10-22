using MySql.Data.MySqlClient;
using System.Configuration;

namespace KalorieAdmin.Classes.Common
{
    public class Connection
    {
        public static readonly string config = "Server=127.0.0.1;Database=kalorietracker;Uid=root;Pwd=;CharSet=utf8mb4;";

        public static MySqlConnection OpenConnection()
        {
            MySqlConnection connection = new MySqlConnection(config);
            connection.Open();
            return connection;
        }

        public static MySqlDataReader Query(string SQL, MySqlConnection connection)
        {
            return new MySqlCommand(SQL, connection).ExecuteReader();
        }

        public static void CloseConnection(MySqlConnection connection)
        {
            connection.Close();
            MySqlConnection.ClearPool(connection);
        }
    }
}
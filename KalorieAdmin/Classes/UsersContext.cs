using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KalorieAdmin.Classes
{
    public class UsersContext : User
    {
        public UsersContext(int Id, string Username, string Email, string PasswordHash, int DailyCalorieGoal, DateTime CreatedAt)
            : base(Id, Username, Email, PasswordHash, DailyCalorieGoal, CreatedAt) { }

        public static List<UsersContext> Select()
        {
            List<UsersContext> allUsers = new List<UsersContext>();
            string SQL = "SELECT * FROM users;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                allUsers.Add(new UsersContext(
                    Data.GetInt32("id"),
                    Data.GetString("username"),
                    Data.GetString("email"),
                    Data.GetString("password_hash"),
                    Data.GetInt32("daily_calorie_goal"),
                    Data.GetDateTime("created_at")
                ));
            }
            Connection.CloseConnection(connection);
            return allUsers;
        }

        public void Add()
        {
            string SQL = "INSERT INTO users (username, email, password_hash, daily_calorie_goal) VALUES (@Username, @Email, @PasswordHash, @DailyCalorieGoal)";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", this.Username);
                    cmd.Parameters.AddWithValue("@Email", this.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", this.PasswordHash);
                    cmd.Parameters.AddWithValue("@DailyCalorieGoal", this.DailyCalorieGoal);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update()
        {
            string SQL = "UPDATE users SET username = @Username, email = @Email, daily_calorie_goal = @DailyCalorieGoal WHERE id = @Id";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", this.Username);
                    cmd.Parameters.AddWithValue("@Email", this.Email);
                    cmd.Parameters.AddWithValue("@DailyCalorieGoal", this.DailyCalorieGoal);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM users WHERE id = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
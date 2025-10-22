using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KalorieAdmin.Classes
{
    public class AchievementsContext : Achievement
    {
        public AchievementsContext(int Id, int UserId, string Name, string Description, DateTime EarnedAt)
            : base(Id, UserId, Name, Description, EarnedAt) { }

        public static List<AchievementsContext> Select()
        {
            List<AchievementsContext> allAchievements = new List<AchievementsContext>();
            string SQL = @"SELECT a.*, u.username as user_name 
                          FROM achievements a 
                          LEFT JOIN users u ON a.user_id = u.id;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                var achievement = new AchievementsContext(
                    Data.GetInt32("id"),
                    Data.GetInt32("user_id"),
                    Data.GetString("name"),
                    Data.IsDBNull(Data.GetOrdinal("description")) ? "" : Data.GetString("description"),
                    Data.GetDateTime("earned_at")
                );
                achievement.UserName = Data.GetString("user_name");
                allAchievements.Add(achievement);
            }
            Connection.CloseConnection(connection);
            return allAchievements;
        }

        public void Add()
        {
            string SQL = "INSERT INTO achievements (user_id, name, description) VALUES (@UserId, @Name, @Description)";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Description", this.Description ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update()
        {
            string SQL = "UPDATE achievements SET user_id = @UserId, name = @Name, description = @Description WHERE id = @Id";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Description", this.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM achievements WHERE id = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
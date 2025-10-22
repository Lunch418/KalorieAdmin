using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KalorieAdmin.Classes
{
    public class GoalsContext : Goal
    {
        public GoalsContext(int Id, int UserId, string TargetType, decimal? TargetValue, DateTime? StartDate, DateTime? EndDate, bool IsCompleted)
            : base(Id, UserId, TargetType, TargetValue, StartDate, EndDate, IsCompleted) { }

        public static List<GoalsContext> Select()
        {
            List<GoalsContext> allGoals = new List<GoalsContext>();
            string SQL = @"SELECT g.*, u.username as user_name 
                          FROM goals g 
                          LEFT JOIN users u ON g.user_id = u.id;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                var goal = new GoalsContext(
                    Data.GetInt32("id"),
                    Data.GetInt32("user_id"),
                    Data.GetString("target_type"),
                    Data.IsDBNull(Data.GetOrdinal("target_value")) ? null : (decimal?)Data.GetDecimal("target_value"),
                    Data.IsDBNull(Data.GetOrdinal("start_date")) ? null : (DateTime?)Data.GetDateTime("start_date"),
                    Data.IsDBNull(Data.GetOrdinal("end_date")) ? null : (DateTime?)Data.GetDateTime("end_date"),
                    Data.GetBoolean("is_completed")
                );
                goal.UserName = Data.GetString("user_name");
                allGoals.Add(goal);
            }
            Connection.CloseConnection(connection);
            return allGoals;
        }

        public void Add()
        {
            string SQL = "INSERT INTO goals (user_id, target_type, target_value, start_date, end_date, is_completed) VALUES (@UserId, @TargetType, @TargetValue, @StartDate, @EndDate, @IsCompleted)";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@TargetType", this.TargetType);
                    cmd.Parameters.AddWithValue("@TargetValue", this.TargetValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsCompleted", this.IsCompleted);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update()
        {
            string SQL = "UPDATE goals SET user_id = @UserId, target_type = @TargetType, target_value = @TargetValue, start_date = @StartDate, end_date = @EndDate, is_completed = @IsCompleted WHERE id = @Id";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@TargetType", this.TargetType);
                    cmd.Parameters.AddWithValue("@TargetValue", this.TargetValue ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@StartDate", this.StartDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@EndDate", this.EndDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsCompleted", this.IsCompleted);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM goals WHERE id = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
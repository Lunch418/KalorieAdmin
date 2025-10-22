using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KalorieAdmin.Classes
{
    public class MealsContext : Meal
    {
        public MealsContext(int Id, int UserId, int ProductId, decimal WeightGrams, DateTime ConsumedAt)
            : base(Id, UserId, ProductId, WeightGrams, ConsumedAt) { }

        public static List<MealsContext> Select()
        {
            List<MealsContext> allMeals = new List<MealsContext>();
            string SQL = @"SELECT m.*, u.username as user_name, p.name as product_name 
                          FROM meals m 
                          LEFT JOIN users u ON m.user_id = u.id 
                          LEFT JOIN products p ON m.product_id = p.id;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                var meal = new MealsContext(
                    Data.GetInt32("id"),
                    Data.GetInt32("user_id"),
                    Data.GetInt32("product_id"),
                    Data.GetDecimal("weight_grams"),
                    Data.GetDateTime("consumed_at")
                );
                meal.UserName = Data.GetString("user_name");
                meal.ProductName = Data.GetString("product_name");
                allMeals.Add(meal);
            }
            Connection.CloseConnection(connection);
            return allMeals;
        }

        public void Add()
        {
            string SQL = "INSERT INTO meals (user_id, product_id, weight_grams) VALUES (@UserId, @ProductId, @WeightGrams)";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@ProductId", this.ProductId);
                    cmd.Parameters.AddWithValue("@WeightGrams", this.WeightGrams);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update()
        {
            string SQL = "UPDATE meals SET user_id = @UserId, product_id = @ProductId, weight_grams = @WeightGrams WHERE id = @Id";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", this.UserId);
                    cmd.Parameters.AddWithValue("@ProductId", this.ProductId);
                    cmd.Parameters.AddWithValue("@WeightGrams", this.WeightGrams);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM meals WHERE id = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
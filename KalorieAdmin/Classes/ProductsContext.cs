using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace KalorieAdmin.Classes
{
    public class ProductsContext : Product
    {
        public ProductsContext(int Id, string Name, decimal Calories, decimal? Proteins, decimal? Fats, decimal? Carbs)
            : base(Id, Name, Calories, Proteins, Fats, Carbs) { }

        public static List<ProductsContext> Select()
        {
            List<ProductsContext> allProducts = new List<ProductsContext>();
            string SQL = "SELECT * FROM products;";
            MySqlConnection connection = Connection.OpenConnection();
            MySqlDataReader Data = Connection.Query(SQL, connection);
            while (Data.Read())
            {
                allProducts.Add(new ProductsContext(
                    Data.GetInt32("id"),
                    Data.GetString("name"),
                    Data.GetDecimal("calories"),
                    Data.IsDBNull(Data.GetOrdinal("proteins")) ? null : (decimal?)Data.GetDecimal("proteins"),
                    Data.IsDBNull(Data.GetOrdinal("fats")) ? null : (decimal?)Data.GetDecimal("fats"),
                    Data.IsDBNull(Data.GetOrdinal("carbs")) ? null : (decimal?)Data.GetDecimal("carbs")
                ));
            }
            Connection.CloseConnection(connection);
            return allProducts;
        }

        public void Add()
        {
            string SQL = "INSERT INTO products (name, calories, proteins, fats, carbs) VALUES (@Name, @Calories, @Proteins, @Fats, @Carbs)";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Calories", this.Calories);
                    cmd.Parameters.AddWithValue("@Proteins", this.Proteins ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fats", this.Fats ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Carbs", this.Carbs ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update()
        {
            string SQL = "UPDATE products SET name = @Name, calories = @Calories, proteins = @Proteins, fats = @Fats, carbs = @Carbs WHERE id = @Id";

            using (MySqlConnection connection = Connection.OpenConnection())
            {
                using (MySqlCommand cmd = new MySqlCommand(SQL, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", this.Name);
                    cmd.Parameters.AddWithValue("@Calories", this.Calories);
                    cmd.Parameters.AddWithValue("@Proteins", this.Proteins ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Fats", this.Fats ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Carbs", this.Carbs ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Id", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete()
        {
            string SQL = $"DELETE FROM products WHERE id = {this.Id}";
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query(SQL, connection);
            Connection.CloseConnection(connection);
        }
    }
}
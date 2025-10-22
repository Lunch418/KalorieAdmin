using KalorieAdmin.Classes.Common;
using KalorieAdmin.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;

namespace KalorieAdmin.Classes
{
    public class ProductsContext
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal? Proteins { get; set; }
        public decimal? Fats { get; set; }
        public decimal? Carbs { get; set; }

        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5000/api/";

        public static async Task<List<ProductsContext>> GetAllProductsAsync()
        {
            List<ProductsContext> products = new List<ProductsContext>();
            var response = await client.GetStringAsync(apiUrl + "products");

            if (!string.IsNullOrEmpty(response))
            {
                products = JsonConvert.DeserializeObject<List<ProductsContext>>(response);
            }
            return products;
        }

        public async Task SaveProductAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "products", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync(apiUrl + "products/" + this.Id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync()
        {
            var response = await client.DeleteAsync(apiUrl + "products/" + this.Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
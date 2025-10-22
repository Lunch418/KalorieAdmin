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
    public class MealsContext
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal WeightGrams { get; set; }
        public DateTime ConsumedAt { get; set; }

        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5000/api/";

        public static async Task<List<MealsContext>> GetAllMealsAsync()
        {
            List<MealsContext> meals = new List<MealsContext>();
            var response = await client.GetStringAsync(apiUrl + "meals");

            if (!string.IsNullOrEmpty(response))
            {
                meals = JsonConvert.DeserializeObject<List<MealsContext>>(response);
            }
            return meals;
        }

        public async Task SaveMealAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "meals", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateMealAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync(apiUrl + "meals/" + this.Id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteMealAsync()
        {
            var response = await client.DeleteAsync(apiUrl + "meals/" + this.Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
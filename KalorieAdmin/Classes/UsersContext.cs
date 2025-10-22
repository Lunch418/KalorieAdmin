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
    public class UsersContext
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int DailyCalorieGoal { get; set; }
        public DateTime CreatedAt { get; set; }

        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5000/api/";

        public static async Task<List<UsersContext>> GetAllUsersAsync()
        {
            List<UsersContext> users = new List<UsersContext>();
            var response = await client.GetStringAsync(apiUrl + "users");

            if (!string.IsNullOrEmpty(response))
            {
                users = JsonConvert.DeserializeObject<List<UsersContext>>(response);
            }
            return users;
        }

        public async Task SaveUserAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "users", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateUserAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync(apiUrl + "users/" + this.Id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteUserAsync()
        {
            var response = await client.DeleteAsync(apiUrl + "users/" + this.Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
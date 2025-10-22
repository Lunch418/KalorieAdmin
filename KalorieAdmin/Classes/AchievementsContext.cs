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
    public class AchievementsContext
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EarnedAt { get; set; }

        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5000/api/";

        public static async Task<List<AchievementsContext>> GetAllAchievementsAsync()
        {
            List<AchievementsContext> achievements = new List<AchievementsContext>();
            var response = await client.GetStringAsync(apiUrl + "achievements");

            if (!string.IsNullOrEmpty(response))
            {
                achievements = JsonConvert.DeserializeObject<List<AchievementsContext>>(response);
            }
            return achievements;
        }

        public async Task SaveAchievementAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "achievements", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAchievementAsync()
        {
            var content = new StringContent(
                JsonConvert.SerializeObject(this),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await client.PutAsync(apiUrl + "achievements/" + this.Id, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAchievementAsync()
        {
            var response = await client.DeleteAsync(apiUrl + "achievements/" + this.Id);
            response.EnsureSuccessStatusCode();
        }
    }
}
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
    namespace KalorieAdmin.Classes
    {
        public class GoalsContext
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public string TargetType { get; set; }
            public decimal? TargetValue { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public bool IsCompleted { get; set; }

            // Приватный клиент для HTTP запросов
            private static readonly HttpClient client = new HttpClient();
            private static readonly string apiUrl = "http://localhost:5000/api/";

            // Получить все цели
            public static async Task<List<GoalsContext>> GetAllGoalsAsync()
            {
                List<GoalsContext> goals = new List<GoalsContext>();
                var response = await client.GetStringAsync(apiUrl + "goals");

                if (!string.IsNullOrEmpty(response))
                {
                    goals = JsonConvert.DeserializeObject<List<GoalsContext>>(response);
                }
                return goals;
            }

            // Сохранить цель
            public async Task SaveGoalAsync()
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(this),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await client.PostAsync(apiUrl + "goals", content);
                response.EnsureSuccessStatusCode();
            }

            // Обновить цель
            public async Task UpdateGoalAsync()
            {
                var content = new StringContent(
                    JsonConvert.SerializeObject(this),
                    System.Text.Encoding.UTF8,
                    "application/json");

                var response = await client.PutAsync(apiUrl + "goals/" + this.Id, content);
                response.EnsureSuccessStatusCode();
            }

            // Удалить цель
            public async Task DeleteGoalAsync()
            {
                var response = await client.DeleteAsync(apiUrl + "goals/" + this.Id);
                response.EnsureSuccessStatusCode();
            }
        }
    }
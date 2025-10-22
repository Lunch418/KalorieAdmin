using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using KalorieAdmin.Models;

namespace KalorieAdmin.Classes
{
    public class AuthContext
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string apiUrl = "http://localhost:5000/api/";

        // Поля для данных пользователя
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; private set; }

        // Регистрация нового пользователя
        public static async Task<bool> RegisterAsync(string username, string email, string password)
        {
            var registerData = new
            {
                username = username,
                email = email,
                password = password
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(registerData),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "auth/register", content);

            return response.IsSuccessStatusCode;
        }

        // Вход пользователя
        public static async Task<AuthContext> LoginAsync(string email, string password)
        {
            var loginData = new
            {
                email = email,
                password = password
            };

            var content = new StringContent(
                JsonConvert.SerializeObject(loginData),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(apiUrl + "auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var authContext = JsonConvert.DeserializeObject<AuthContext>(responseData);
                return authContext;
            }
            else
            {
                return null;
            }
        }

        // Сохранение JWT токена для использования в запросах
        public void SaveToken(string token)
        {
            this.Token = token;
        }

        // Метод для добавления токена в заголовки для авторизованных запросов
        public static void SetAuthorizationHeader(HttpClient client, string token)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}

using System;

namespace KalorieAdmin.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public int DailyCalorieGoal { get; set; }
        public DateTime CreatedAt { get; set; }

        public User() { }

        public User(int Id, string Username, string Email, string PasswordHash, int DailyCalorieGoal, DateTime CreatedAt)
        {
            this.Id = Id;
            this.Username = Username;
            this.Email = Email;
            this.PasswordHash = PasswordHash;
            this.DailyCalorieGoal = DailyCalorieGoal;
            this.CreatedAt = CreatedAt;
        }
    }
}
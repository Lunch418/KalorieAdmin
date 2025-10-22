using System;

namespace KalorieAdmin.Models
{
    public class Achievement
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EarnedAt { get; set; }
        public string UserName { get; set; }

        public Achievement() { }

        public Achievement(int Id, int UserId, string Name, string Description, DateTime EarnedAt)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.Name = Name;
            this.Description = Description;
            this.EarnedAt = EarnedAt;
        }
    }
}
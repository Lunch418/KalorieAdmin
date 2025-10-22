using System;

namespace KalorieAdmin.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TargetType { get; set; }
        public decimal? TargetValue { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCompleted { get; set; }
        public string UserName { get; set; }

        public Goal() { }

        public Goal(int Id, int UserId, string TargetType, decimal? TargetValue, DateTime? StartDate, DateTime? EndDate, bool IsCompleted)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.TargetType = TargetType;
            this.TargetValue = TargetValue;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.IsCompleted = IsCompleted;
        }
    }
}
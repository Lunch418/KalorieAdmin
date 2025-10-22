using System;

namespace KalorieAdmin.Models
{
    public class Meal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal WeightGrams { get; set; }
        public DateTime ConsumedAt { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }

        public Meal() { }

        public Meal(int Id, int UserId, int ProductId, decimal WeightGrams, DateTime ConsumedAt)
        {
            this.Id = Id;
            this.UserId = UserId;
            this.ProductId = ProductId;
            this.WeightGrams = WeightGrams;
            this.ConsumedAt = ConsumedAt;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalorieAdmin.Models
{
    public class MealDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal WeightGrams { get; set; }
        public DateTime ConsumedAt { get; set; }
    }
}

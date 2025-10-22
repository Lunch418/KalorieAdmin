using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KalorieAdmin.Models
{
    public class GoalDto
    {
        public int UserId { get; set; }
        public string TargetType { get; set; }
        public decimal TargetValue { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}

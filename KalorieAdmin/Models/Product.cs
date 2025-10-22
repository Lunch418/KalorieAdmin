namespace KalorieAdmin.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Calories { get; set; }
        public decimal? Proteins { get; set; }
        public decimal? Fats { get; set; }
        public decimal? Carbs { get; set; }

        public Product() { }

        public Product(int Id, string Name, decimal Calories, decimal? Proteins, decimal? Fats, decimal? Carbs)
        {
            this.Id = Id;
            this.Name = Name;
            this.Calories = Calories;
            this.Proteins = Proteins;
            this.Fats = Fats;
            this.Carbs = Carbs;
        }
    }
}
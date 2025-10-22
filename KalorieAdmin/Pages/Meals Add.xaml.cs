using KalorieAdmin.Classes;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Meals
{
    public partial class Add : Page
    {
        private MealsContext Meal;

        public Add(MealsContext meal = null)
        {
            InitializeComponent();
            this.Meal = meal;

            if (meal != null)
            {
                UserId.Text = meal.UserId.ToString();
                ProductId.Text = meal.ProductId.ToString();
                WeightGrams.Text = meal.WeightGrams.ToString();
                btnSave.Content = "Обновить";
            }
        }

        private void SaveMeal(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserId.Text) || 
                string.IsNullOrWhiteSpace(ProductId.Text) ||
                string.IsNullOrWhiteSpace(WeightGrams.Text))
            {
                MessageBox.Show("Заполните все обязательные поля");
                return;
            }

            if (!int.TryParse(UserId.Text, out int userId))
            {
                MessageBox.Show("ID пользователя должен быть числом");
                return;
            }

            if (!int.TryParse(ProductId.Text, out int productId))
            {
                MessageBox.Show("ID продукта должен быть числом");
                return;
            }

            if (!decimal.TryParse(WeightGrams.Text, out decimal weightGrams))
            {
                MessageBox.Show("Вес должен быть числом");
                return;
            }

            if (this.Meal == null)
            {
                // Добавление
                MealsContext newMeal = new MealsContext(
                    0,
                    userId,
                    productId,
                    weightGrams,
                    System.DateTime.Now
                );
                newMeal.Add();
                MessageBox.Show("Прием пищи добавлен");
            }
            else
            {
                // Обновление
                Meal = new MealsContext(
                    Meal.Id,
                    userId,
                    productId,
                    weightGrams,
                    Meal.ConsumedAt
                );
                Meal.Update();
                MessageBox.Show("Прием пищи обновлен");
            }

            MainWindow.init.OpenPage(new Main());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Main());
        }
    }
}
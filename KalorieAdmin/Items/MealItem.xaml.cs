using KalorieAdmin.Classes;
using KalorieAdmin.Pages.Meals;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Items
{
    public partial class MealItem : UserControl
    {
        public MealsContext Meal { get; set; }
        public Main MainPage { get; set; }

        public MealItem(MealsContext meal, Main mainPage)
        {
            InitializeComponent();
            this.Meal = meal;
            this.MainPage = mainPage;
            this.DataContext = Meal;
        }

        private void EditMeal(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add(this.Meal));
        }

        private void DeleteMeal(object sender, RoutedEventArgs e)
        {
            Meal.Delete();
            MainPage.AllMeals.Remove(Meal);
            MainPage.UpdateMealsList();
        }
    }
}
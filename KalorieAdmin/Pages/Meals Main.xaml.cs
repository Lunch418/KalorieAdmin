using KalorieAdmin.Classes;
using KalorieAdmin.Items;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Meals
{
    public partial class Main : Page
    {
        public List<MealsContext> AllMeals = MealsContext.Select();
        List<MealsContext> FilteredMeals;

        public Main()
        {
            InitializeComponent();
            FilteredMeals = AllMeals;
            UpdateMealsList();
        }

        public void UpdateMealsList()
        {
            parent.Children.Clear();
            foreach (MealsContext item in FilteredMeals)
            {
                parent.Children.Add(new MealItem(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            FilteredMeals = AllMeals.Where(m =>
                (string.IsNullOrEmpty(FilterUser.Text) || FilterUser.Text == "ID пользователя" || m.UserId.ToString().Contains(FilterUser.Text)) &&
                (string.IsNullOrEmpty(FilterProduct.Text) || FilterProduct.Text == "ID продукта" || m.ProductId.ToString().Contains(FilterProduct.Text))
            ).ToList();

            UpdateMealsList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterUser.Text = "ID пользователя";
            FilterProduct.Text = "ID продукта";
            FilteredMeals = AllMeals;
            UpdateMealsList();
        }

        private void AddMeal(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }
    }
}
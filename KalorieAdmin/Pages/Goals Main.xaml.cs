using KalorieAdmin.Classes;
using KalorieAdmin.Items;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KalorieAdmin.Pages.Users; // Для страницы добавления пользователя


namespace KalorieAdmin.Pages.Goals
{
    public partial class Main : Page
    {
        public List<GoalsContext> AllGoals = GoalsContext.Select();
        List<GoalsContext> FilteredGoals;

        public Main()
        {
            InitializeComponent();
            FilteredGoals = AllGoals;
            UpdateGoalsList();
            FilterType.SelectedIndex = 0;
        }

        public void UpdateGoalsList()
        {
            parent.Children.Clear();
            foreach (GoalsContext item in FilteredGoals)
            {
                parent.Children.Add(new GoalItem(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            var selectedType = (FilterType.SelectedItem as ComboBoxItem)?.Content.ToString();

            FilteredGoals = AllGoals.Where(g =>
                (string.IsNullOrEmpty(FilterUser.Text) || FilterUser.Text == "ID пользователя" || g.UserId.ToString().Contains(FilterUser.Text)) &&
                (selectedType == "Все типы" || string.IsNullOrEmpty(selectedType) || g.TargetType == selectedType)
            ).ToList();

            UpdateGoalsList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterUser.Text = "ID пользователя";
            FilterType.SelectedIndex = 0;
            FilteredGoals = AllGoals;
            UpdateGoalsList();
        }

        private void AddGoal(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }
    }
}
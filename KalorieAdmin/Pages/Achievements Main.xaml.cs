using KalorieAdmin.Classes;
using KalorieAdmin.Items;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Achievements
{
    public partial class Main : Page
    {
        public List<AchievementsContext> AllAchievements = AchievementsContext.Select();
        List<AchievementsContext> FilteredAchievements;

        public Main()
        {
            InitializeComponent();
            FilteredAchievements = AllAchievements;
            UpdateAchievementsList();
        }

        public void UpdateAchievementsList()
        {
            parent.Children.Clear();
            foreach (AchievementsContext item in FilteredAchievements)
            {
                parent.Children.Add(new AchievementItem(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            FilteredAchievements = AllAchievements.Where(a =>
                (string.IsNullOrEmpty(FilterUser.Text) || FilterUser.Text == "ID пользователя" || a.UserId.ToString().Contains(FilterUser.Text)) &&
                (string.IsNullOrEmpty(FilterName.Text) || FilterName.Text == "Поиск по названию" || a.Name.Contains(FilterName.Text))
            ).ToList();

            UpdateAchievementsList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterUser.Text = "ID пользователя";
            FilterName.Text = "Поиск по названию";
            FilteredAchievements = AllAchievements;
            UpdateAchievementsList();
        }

        private void AddAchievement(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }
    }
}
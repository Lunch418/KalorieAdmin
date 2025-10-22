using KalorieAdmin.Classes;
using KalorieAdmin.Pages.Achievements;
using System.Windows;
using System.Windows.Controls;
using KalorieAdmin.Pages.Achievements; 

namespace KalorieAdmin.Items
{
    public partial class AchievementItem : UserControl
    {
        public AchievementsContext Achievement { get; set; }
        public Main MainPage { get; set; }

        public AchievementItem(AchievementsContext achievement, Main mainPage)
        {
            InitializeComponent();
            this.Achievement = achievement;
            this.MainPage = mainPage;
            this.DataContext = Achievement;
        }

        private void EditAchievement(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());  // Открытие страницы Add для достижения
        }

        private void DeleteAchievement(object sender, RoutedEventArgs e)
        {
            Achievement.Delete();
            MainPage.AllAchievements.Remove(Achievement);
            MainPage.UpdateAchievementsList();
        }
    }
}

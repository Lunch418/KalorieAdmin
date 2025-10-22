using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin
{
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPage(new Pages.Users.Main());
        }

        public void OpenPage(Page Page)
        {
            Frame.Navigate(Page);
        }

        private void OpenUsers(object sender, RoutedEventArgs e) =>
            OpenPage(new Pages.Users.Main());

        private void OpenProducts(object sender, RoutedEventArgs e) =>
            OpenPage(new Pages.Products.Main());

        private void OpenMeals(object sender, RoutedEventArgs e) =>
            OpenPage(new Pages.Meals.Main());

        private void OpenGoals(object sender, RoutedEventArgs e) =>
            OpenPage(new Pages.Goals.Main());

        private void OpenAchievements(object sender, RoutedEventArgs e) =>
            OpenPage(new Pages.Achievements.Main());
    }
}
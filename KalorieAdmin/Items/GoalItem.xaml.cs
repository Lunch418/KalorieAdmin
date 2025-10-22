using KalorieAdmin.Classes;
using KalorieAdmin.Pages.Goals;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Items
{
    public partial class GoalItem : UserControl
    {
        public GoalsContext Goal { get; set; }
        public Main MainPage { get; set; }

        public GoalItem(GoalsContext goal, Main mainPage)
        {
            InitializeComponent();
            this.Goal = goal;
            this.MainPage = mainPage;
            this.DataContext = Goal;

            // Устанавливаем текст для периода
            if (goal.StartDate.HasValue && goal.EndDate.HasValue)
                Period.Text = $"{goal.StartDate.Value:dd.MM.yyyy} - {goal.EndDate.Value:dd.MM.yyyy}";
            else if (goal.StartDate.HasValue)
                Period.Text = $"{goal.StartDate.Value:dd.MM.yyyy} - ...";
            else
                Period.Text = "Не указано";
        }

        private void EditGoal(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add(this.Goal));
        }

        private void DeleteGoal(object sender, RoutedEventArgs e)
        {
            Goal.Delete();
            MainPage.AllGoals.Remove(Goal);
            MainPage.UpdateGoalsList();
        }
    }
}
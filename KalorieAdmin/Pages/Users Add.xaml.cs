using KalorieAdmin.Classes;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Users
{
    public partial class Add : Page
    {
        private UsersContext User;

        public Add(UsersContext user = null)
        {
            InitializeComponent();
            this.User = user;

            if (user != null)
            {
                Username.Text = user.Username;
                Email.Text = user.Email;
                DailyCalorieGoal.Text = user.DailyCalorieGoal.ToString();
                btnSave.Content = "Обновить";
            }
        }

        private void SaveUser(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Username.Text) || 
                string.IsNullOrWhiteSpace(Email.Text) ||
                string.IsNullOrWhiteSpace(DailyCalorieGoal.Text))
            {
                MessageBox.Show("Заполните все обязательные поля");
                return;
            }

            if (!int.TryParse(DailyCalorieGoal.Text, out int calorieGoal))
            {
                MessageBox.Show("Цель по калориям должна быть числом");
                return;
            }

            if (this.User == null)
            {
                // Добавление
                UsersContext newUser = new UsersContext(
                    0,
                    Username.Text,
                    Email.Text,
                    "temp_hash", // В реальном приложении хешируйте пароль
                    calorieGoal,
                    System.DateTime.Now
                );
                newUser.Add();
                MessageBox.Show("Пользователь добавлен");
            }
            else
            {
                // Обновление
                User = new UsersContext(
                    User.Id,
                    Username.Text,
                    Email.Text,
                    User.PasswordHash,
                    calorieGoal,
                    User.CreatedAt
                );
                User.Update();
                MessageBox.Show("Пользователь обновлен");
            }

            MainWindow.init.OpenPage(new Main());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Main());
        }
    }
}
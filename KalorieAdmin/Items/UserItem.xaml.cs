using KalorieAdmin.Classes;
using KalorieAdmin.Pages.Users;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Items
{
    public partial class UserItem : UserControl
    {
        public UsersContext User { get; set; }
        public Main MainPage { get; set; }

        public UserItem(UsersContext user, Main mainPage)
        {
            InitializeComponent();
            this.User = user;
            this.MainPage = mainPage;
            this.DataContext = User;
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add(this.User));
        }

        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            User.Delete();
            MainPage.AllUsers.Remove(User);
            MainPage.UpdateUsersList();
        }
    }
}
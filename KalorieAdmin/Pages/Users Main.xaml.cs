using KalorieAdmin.Classes;
using KalorieAdmin.Items;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Users
{
    public partial class Main : Page
    {
        public List<UsersContext> AllUsers = UsersContext.Select();
        List<UsersContext> FilteredUsers;

        public Main()
        {
            InitializeComponent();
            FilteredUsers = AllUsers;
            UpdateUsersList();
        }

        public void UpdateUsersList()
        {
            parent.Children.Clear();
            foreach (UsersContext item in FilteredUsers)
            {
                parent.Children.Add(new UserItem(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            FilteredUsers = AllUsers.Where(u =>
                (string.IsNullOrEmpty(FilterUsername.Text) || FilterUsername.Text == "Поиск по имени" || u.Username.Contains(FilterUsername.Text)) &&
                (string.IsNullOrEmpty(FilterEmail.Text) || FilterEmail.Text == "Поиск по email" || u.Email.Contains(FilterEmail.Text))
            ).ToList();

            UpdateUsersList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterUsername.Text = "Поиск по имени";
            FilterEmail.Text = "Поиск по email";
            FilteredUsers = AllUsers;
            UpdateUsersList();
        }

        private void AddUser(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }
    }
}
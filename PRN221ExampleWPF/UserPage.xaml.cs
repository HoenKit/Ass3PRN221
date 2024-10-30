using BusinessObject.Models;
using DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PRN221ExampleWPF
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private readonly UserRepository _UserRepository;
        private readonly CategoryRepository _CategoryRepository;
        public UserPage(UserRepository UserRepository, CategoryRepository categoryRepository)
        {
            _UserRepository = UserRepository;
            _CategoryRepository = categoryRepository;
            InitializeComponent();
            Loaded();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(UserIDTextBox.Text))
            {
                var User = new User();
                User.UserName = UserNameTextBox.Text;
                User.Password = PasswordTextBox.Password;
                _UserRepository.CreateUser(User);
                Loaded();
            }
            else
            {
                int id = int.Parse(UserIDTextBox.Text);
                var User = _UserRepository.GetUserById(id);
                User.UserName = UserNameTextBox.Text;
                User.Password = PasswordTextBox.Password;
                _UserRepository.UpdateUser(User);
                Loaded();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(UserIDTextBox.Text);
            _UserRepository.DeleteUser(id);
            Loaded();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset các trường dữ liệu trong form
            UserIDTextBox.Clear();
            UserNameTextBox.Clear();
            PasswordTextBox.Clear();
            Loaded();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchTextBox.Text;
            UserDataGrid.ItemsSource = _UserRepository.SearchByName(search);
        }

        private void UserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loaded();
            if (UserDataGrid.SelectedItem is User selectedUser)
            {
                UserIDTextBox.Text = selectedUser.Id.ToString();
                UserNameTextBox.Text = selectedUser.UserName;
            }
        }

        private void Loaded()
        {
            UserDataGrid.ItemsSource = _UserRepository.GetUsersList();
        }
    }
}

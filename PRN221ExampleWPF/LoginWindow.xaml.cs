using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Shapes;

namespace PRN221ExampleWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly UserRepository _userRepository;
        private readonly IServiceProvider _serviceProvider;
        public LoginWindow( UserRepository userRepository, IServiceProvider serviceProvider)
        {
            _userRepository = userRepository;
            InitializeComponent();
            _serviceProvider = serviceProvider;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            var login = _userRepository.Login(username, password);
            if (login == true)
            {
                MessageBox.Show("Login successful!");
                var categoryWindow = _serviceProvider.GetService<MainWindow>();
                categoryWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Login failed!");
            }
        }
    }
}

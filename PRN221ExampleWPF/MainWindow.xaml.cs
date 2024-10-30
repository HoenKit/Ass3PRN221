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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PRN221ExampleWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly CategoryRepository _categoryRepository;
        private readonly BookRepository _bookRepository;
        private readonly UserRepository _userRepository;
        private readonly ShipRepository _shipRepository;
        public MainWindow(IServiceProvider serviceProvider, CategoryRepository categoryRepository, BookRepository bookRepository, UserRepository userRepository, ShipRepository shipRepository)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _categoryRepository = categoryRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _shipRepository = shipRepository;
        }

        private void MenuItem_Category_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoryPage(_categoryRepository));
        }

        private void MenuItem_Book_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BookPage(_bookRepository, _categoryRepository));
        }

        private void MenuItem_User_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserPage(_userRepository, _categoryRepository));
        }

        private void MenuItem_Ship_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ShipPage(_shipRepository, _categoryRepository, _bookRepository, _userRepository));
        }
    }
}

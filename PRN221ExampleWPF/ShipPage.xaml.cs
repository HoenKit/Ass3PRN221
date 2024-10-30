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
    /// Interaction logic for ShipPage.xaml
    /// </summary>
    public partial class ShipPage : Page
    {
        private readonly ShipRepository _ShipRepository;
        private readonly CategoryRepository _CategoryRepository;
        private readonly BookRepository _BookRepository;  
        private readonly UserRepository _UserRepository;
        public ShipPage(ShipRepository ShipRepository, CategoryRepository categoryRepository, BookRepository bookRepository, UserRepository userRepository)
        {
            _ShipRepository = ShipRepository;
            _CategoryRepository = categoryRepository;
            InitializeComponent();
            _BookRepository = bookRepository;
            _UserRepository = userRepository;
            Loaded();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ShipIDTextBox.Text))
            {
                var Ship = new Ship();
                if (BookCmbBox.SelectedValue is int bookId)
                {
                    Ship.Book = _BookRepository.GetBookById(bookId);
                    Ship.BookId = bookId;
                }
                else
                {
                    MessageBox.Show("Please select a book.");
                    return;
                }
                if (UserOrderCmbBox.SelectedValue is int userOrderId)
                {
                    Ship.UserOrder = _UserRepository.GetUserById(userOrderId);
                    Ship.UserOrderId = userOrderId;
                }
                else
                {
                    MessageBox.Show("Please select a user.");
                    return;
                }
                if (UserShipCmbBox.SelectedValue is int userShipId)
                {
                    Ship.UserShip = _UserRepository.GetUserById(userShipId);
                    Ship.UserShipId = userShipId;
                }
                else
                {
                    MessageBox.Show("Please select a user.");
                    return;
                }
                Ship.DateShip = dpDateShip.SelectedDate.Value;
                Ship.DateOrder = dpDateOrder.SelectedDate.Value;
                _ShipRepository.CreateShip(Ship);
                Loaded();
            }
            else
            {
                int id = int.Parse(ShipIDTextBox.Text);
                var Ship = _ShipRepository.GetShipById(id);
                if (BookCmbBox.SelectedValue is int bookId)
                {
                    Ship.Book = _BookRepository.GetBookById(bookId);
                    Ship.BookId = bookId;
                }
                else
                {
                    MessageBox.Show("Please select a book.");
                    return;
                }
                if (UserOrderCmbBox.SelectedValue is int userOrderId)
                {
                    Ship.UserOrder = _UserRepository.GetUserById(userOrderId);
                    Ship.UserOrderId = userOrderId;
                }
                else
                {
                    MessageBox.Show("Please select a user.");
                    return;
                }
                if (UserShipCmbBox.SelectedValue is int userShipId)
                {
                    Ship.UserShip = _UserRepository.GetUserById(userShipId);
                    Ship.UserShipId = userShipId;
                }
                else
                {
                    MessageBox.Show("Please select a user.");
                    return;
                }
                Ship.DateShip = dpDateShip.SelectedDate.Value;
                Ship.DateOrder = dpDateOrder.SelectedDate.Value;
                _ShipRepository.UpdateShip(Ship);
                Loaded();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(ShipIDTextBox.Text);
            _ShipRepository.DeleteShip(id);
            Loaded();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset các trường dữ liệu trong form
            ShipIDTextBox.Clear();
            dpDateShip.SelectedDate = null;
            dpDateOrder.SelectedDate = null;
            Loaded();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int search = int.Parse(SearchTextBox.Text);
            ShipDataGrid.ItemsSource = _ShipRepository.SearchById(search);
        }

        private void ShipDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loaded();
            if (ShipDataGrid.SelectedItem is Ship selectedShip)
            {
                ShipIDTextBox.Text = selectedShip.Id.ToString();
                BookCmbBox.Text = selectedShip.Book.ToString();
                UserOrderCmbBox.Text = selectedShip.UserOrder.ToString();
                UserShipCmbBox.Text = selectedShip.UserShip.ToString();
                dpDateOrder.Text = selectedShip.DateOrder.ToString();
                dpDateShip.Text = selectedShip.DateShip.ToString();
            }
        }

        private void Loaded()
        {
            ShipDataGrid.ItemsSource = _ShipRepository.GetShipsList();
            BookCmbBox.ItemsSource = _BookRepository.GetBooksList();
            UserOrderCmbBox.ItemsSource = _UserRepository.GetUsersList();
            UserShipCmbBox.ItemsSource = _UserRepository.GetUsersList();
        }
    }
}

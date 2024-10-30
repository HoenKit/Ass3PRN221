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
using static System.Net.Mime.MediaTypeNames;

namespace PRN221ExampleWPF
{
    /// <summary>
    /// Interaction logic for BookPage.xaml
    /// </summary>
    public partial class BookPage : Page
    {
        private readonly BookRepository _BookRepository;
        private readonly CategoryRepository _CategoryRepository;
        public BookPage(BookRepository BookRepository, CategoryRepository categoryRepository)
        {
            _BookRepository = BookRepository;
            _CategoryRepository = categoryRepository;
            InitializeComponent();
            Loaded();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BookIDTextBox.Text))
            {
                var Book = new Book();
                Book.BookName = BookNameTextBox.Text;
                Book.Price = int.Parse(PriceTextBox.Text);
                if (CategoryCmbBox.SelectedValue is int categoryId)
                {
                    Book.Category = _CategoryRepository.GetCategoryById(categoryId);
                }
                else
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }
                Book.Category = _CategoryRepository.GetCategoryById(categoryId);
                _BookRepository.CreateBook(Book);
                Loaded();
            }
            else
            {
                int id = int.Parse(BookIDTextBox.Text);
                var Book = _BookRepository.GetBookById(id);
                Book.BookName = BookNameTextBox.Text;
                Book.Price = int.Parse(PriceTextBox.Text);
                if (CategoryCmbBox.SelectedValue is int categoryId)
                {
                    Book.Category = _CategoryRepository.GetCategoryById(categoryId);
                }
                else
                {
                    MessageBox.Show("Please select a category.");
                    return;
                }
                _BookRepository.UpdateBook(Book);
                Loaded();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(BookIDTextBox.Text);
            _BookRepository.DeleteBook(id);
            Loaded();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset các trường dữ liệu trong form
            BookIDTextBox.Clear();
            BookNameTextBox.Clear();
            PriceTextBox.Clear();
            Loaded();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchTextBox.Text;
            BookDataGrid.ItemsSource = _BookRepository.SearchByName(search);
        }

        private void BookDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loaded();
            if (BookDataGrid.SelectedItem is Book selectedBook)
            {
                BookIDTextBox.Text = selectedBook.Id.ToString();
                BookNameTextBox.Text = selectedBook.BookName;
                PriceTextBox.Text = selectedBook.Price.ToString();
                CategoryCmbBox.Text = selectedBook.Category.ToString();
            }
        }

        private void Loaded()
        {
            BookDataGrid.ItemsSource = _BookRepository.GetBooksList();
            CategoryCmbBox.ItemsSource = _CategoryRepository.GetCategoriesList();
        }
    }
}


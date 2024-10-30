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
    /// Interaction logic for CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {
        private readonly CategoryRepository _categoryRepository;
        public CategoryPage(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            InitializeComponent();
            Loaded();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(CategoryIDTextBox.Text))
            {
                var category = new Category();
                category.CategoryName = CategoryNameTextBox.Text;
                _categoryRepository.CreateCategory(category);
                Loaded();
            }
            else
            {
                int id = int.Parse(CategoryIDTextBox.Text);
                var category = _categoryRepository.GetCategoryById(id);
                category.CategoryName = CategoryNameTextBox.Text;
                _categoryRepository.UpdateCategory(category);
                Loaded();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(CategoryIDTextBox.Text);
            _categoryRepository.DeleteCategory(id);
            Loaded();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Reset các trường dữ liệu trong form
            CategoryIDTextBox.Clear();
            CategoryNameTextBox.Clear();
            Loaded();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string search = SearchTextBox.Text;
            CategoryDataGrid.ItemsSource = _categoryRepository.SearchByName(search);
        }

        private void CategoryDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Loaded();
            if (CategoryDataGrid.SelectedItem is Category selectedCategory)
            {
                // Hiển thị thông tin người dùng được chọn trong các TextBox
                CategoryIDTextBox.Text = selectedCategory.Id.ToString();
                CategoryNameTextBox.Text = selectedCategory.CategoryName;
            }
        }

        private void Loaded()
        {
            CategoryDataGrid.ItemsSource = _categoryRepository.GetCategoriesList();
        }
    }
}


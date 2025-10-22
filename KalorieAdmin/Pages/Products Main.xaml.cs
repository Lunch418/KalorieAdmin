using KalorieAdmin.Classes;
using KalorieAdmin.Items;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Products
{
    public partial class Main : Page
    {
        public List<ProductsContext> AllProducts = ProductsContext.Select();
        List<ProductsContext> FilteredProducts;

        public Main()
        {
            InitializeComponent();
            FilteredProducts = AllProducts;
            UpdateProductsList();
        }

        public void UpdateProductsList()
        {
            parent.Children.Clear();
            foreach (ProductsContext item in FilteredProducts)
            {
                parent.Children.Add(new ProductItem(item, this));
            }
        }

        private void ApplyFilter(object sender, RoutedEventArgs e)
        {
            FilteredProducts = AllProducts.Where(p =>
                string.IsNullOrEmpty(FilterName.Text) || FilterName.Text == "Поиск по названию" || p.Name.Contains(FilterName.Text)
            ).ToList();

            UpdateProductsList();
        }

        private void ResetFilter(object sender, RoutedEventArgs e)
        {
            FilterName.Text = "Поиск по названию";
            FilteredProducts = AllProducts;
            UpdateProductsList();
        }

        private void AddProduct(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add());
        }
    }
}
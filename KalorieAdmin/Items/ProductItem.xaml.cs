using KalorieAdmin.Classes;
using KalorieAdmin.Pages.Products;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Items
{
    public partial class ProductItem : UserControl
    {
        public ProductsContext Product { get; set; }
        public Main MainPage { get; set; }

        public ProductItem(ProductsContext product, Main mainPage)
        {
            InitializeComponent();
            this.Product = product;
            this.MainPage = mainPage;
            this.DataContext = Product;
        }

        private void EditProduct(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Add(this.Product));
        }

        private void DeleteProduct(object sender, RoutedEventArgs e)
        {
            Product.Delete();
            MainPage.AllProducts.Remove(Product);
            MainPage.UpdateProductsList();
        }
    }
}
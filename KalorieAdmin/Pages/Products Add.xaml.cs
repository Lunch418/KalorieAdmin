using KalorieAdmin.Classes;
using System.Windows;
using System.Windows.Controls;

namespace KalorieAdmin.Pages.Products
{
    public partial class Add : Page
    {
        private ProductsContext Product;

        public Add(ProductsContext product = null)
        {
            InitializeComponent();
            this.Product = product;

            if (product != null)
            {
                ProductName.Text = product.Name;
                Calories.Text = product.Calories.ToString();
                Proteins.Text = product.Proteins?.ToString();
                Fats.Text = product.Fats?.ToString();
                Carbs.Text = product.Carbs?.ToString();
                btnSave.Content = "Обновить";
            }
        }

        private void SaveProduct(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ProductName.Text) || 
                string.IsNullOrWhiteSpace(Calories.Text))
            {
                MessageBox.Show("Заполните название и калории");
                return;
            }

            if (!decimal.TryParse(Calories.Text, out decimal calories))
            {
                MessageBox.Show("Калории должны быть числом");
                return;
            }

            decimal? proteins = null;
            decimal? fats = null;
            decimal? carbs = null;

            if (!string.IsNullOrWhiteSpace(Proteins.Text) && decimal.TryParse(Proteins.Text, out decimal parsedProteins))
                proteins = parsedProteins;

            if (!string.IsNullOrWhiteSpace(Fats.Text) && decimal.TryParse(Fats.Text, out decimal parsedFats))
                fats = parsedFats;

            if (!string.IsNullOrWhiteSpace(Carbs.Text) && decimal.TryParse(Carbs.Text, out decimal parsedCarbs))
                carbs = parsedCarbs;

            if (this.Product == null)
            {
                // Добавление
                ProductsContext newProduct = new ProductsContext(
                    0,
                    ProductName.Text,
                    calories,
                    proteins,
                    fats,
                    carbs
                );
                newProduct.Add();
                MessageBox.Show("Продукт добавлен");
            }
            else
            {
                // Обновление
                Product = new ProductsContext(
                    Product.Id,
                    ProductName.Text,
                    calories,
                    proteins,
                    fats,
                    carbs
                );
                Product.Update();
                MessageBox.Show("Продукт обновлен");
            }

            MainWindow.init.OpenPage(new Main());
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPage(new Main());
        }
    }
}
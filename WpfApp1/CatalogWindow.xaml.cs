using BlApi;
using MainWindow;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        private IBl bl;
        public CatalogWindow(IBl BL)
        {
            try
            {
                InitializeComponent();
                bl = BL;
                CategorySelector.ItemsSource = BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
                CatalogListview.ItemsSource = bl.product.GetCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetCart(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new(bl);
            cartWindow.Show();
            this.Close();
        }

        private void AddProductToCart(object sender, MouseButtonEventArgs e)
        {
            new ProductWindow(bl, ((BO.ProductItem)CatalogListview.SelectedItem).ID).Show();
            this.Close();
        }

        private void Categories(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                CatalogListview.ItemsSource = bl.product.GetCatalog((BO.Enums.eCategory)CatalogListview.SelectedItem);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteFilter(object sender, RoutedEventArgs e)
        {
            try
            {
                CatalogListview.ItemsSource = bl.product.GetCatalog();
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }
    }
}

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
using BlApi;
using PL;

namespace MainWindow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl;
        private int p_id;
        public ProductWindow(IBl BL, int? pId = null)
        {
            InitializeComponent();
            bl = BL;
            category.ItemsSource = BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
            if (pId == null)
            {
                p_id = 0;
                addOrUpdate.Content = "add";
                btnDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                addOrUpdate.Content = "update";
                p_id = (int)pId;
                BO.Product product = bl.product.GetProductItemsForManager(p_id);
                productName.Text = product.Name;
                productPrice.Text = product.Price.ToString();
                category.SelectedItem = product.Category;
                productAmount.Text = product.InStock.ToString();
            }
        }
        /// <summary>
        /// add or update the product due to the request
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit(object sender, RoutedEventArgs e)
        {
            BO.Product p = new BO.Product()
            {
                ID = p_id,
                Name = productName.Text,
                Category = (BO.Enums.eCategory)category.SelectedItem,
                InStock = int.Parse(productAmount.Text),
                Price = int.Parse(productPrice.Text)
            };
            if(p_id == 0)
            {
                bl.product.AddProduct(p);
            }
            else
            {
               bl.product.UpdateProduct(p);
            } 
            MessageBox.Show("Done successfully");
        }
        /// <summary>
        /// return the user to thg products window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Back(object sender, RoutedEventArgs e)
        {
            ProductListWindow window = new ProductListWindow(bl);
            window.Show();
            this.Hide();
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            bl.product.RemoveProduct(p_id);
            MessageBox.Show("Done successfully");

        }
    }
}

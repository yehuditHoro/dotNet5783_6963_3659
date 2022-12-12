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
    public partial class Window1 : Window
    {
        private IBl bl;
        public Window1(IBl BL)
        {
            InitializeComponent();
            bl = BL;
            category.ItemsSource = BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p = new BO.Product()
            {
                ID = 0,
                Name = productName.Text,
                Category = (BO.Enums.eCategory)category.SelectedItem,
                InStock = int.Parse(productAmount.Text),
                Price = int.Parse(productPrice.Text)
            };
            bl.product.AddProduct(p);
            MessageBox.Show("Product successfully added");
        }

        private void back(object sender, RoutedEventArgs e)
        {
            Window2 window = new Window2(bl);
            window.Show();
            this.Hide();
        }

        private void productName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}

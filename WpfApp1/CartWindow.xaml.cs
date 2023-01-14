using BlApi;
using BO;
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
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        private IBl bl;
        private BO.Cart c;
        public CartWindow(IBl BL, BO.Cart cart)
        {
            InitializeComponent();
            bl = BL;
            c = cart;
            DataContext = cart;
            //cart.Items.
            //CartListview.ItemsSource = cart.Items;
        }

        private void MakeAnOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.cart.MakeAnOrder(c, (Name.Text), (Email.Text), (Address.Text));

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            try {
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            bl.cart.UpdateQuantity(c, changed.ProductID,0);
            CartWindow cartWindow = new CartWindow(bl, c);
            cartWindow.Show();
            this.Close(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Increase(object sender, RoutedEventArgs e)
        {
            try { 
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            bl.cart.UpdateQuantity(c,changed.ProductID,changed.Amount+1);
            CartWindow cartWindow = new CartWindow(bl,c);
            cartWindow.Show();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Decrease(object sender, RoutedEventArgs e)
        {
            try { 
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            bl.cart.UpdateQuantity(c, changed.ProductID, changed.Amount -1);
            CartWindow cartWindow = new CartWindow(bl, c);
            cartWindow.Show();
            this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

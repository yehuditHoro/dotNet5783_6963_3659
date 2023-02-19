using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Schema;

namespace PL;

/// <summary>
/// Interaction logic for CartWindow.xaml
/// </summary>
public partial class CartWindow : Window
{
    private IBl bl;
    private BO.Cart c;

    private ObservableCollection<BO.OrderItem> coll { get; set; }
    public CartWindow(IBl BL, BO.Cart cart)
    {
        InitializeComponent();
        bl = BL;
        c = cart;
        coll = new ObservableCollection<BO.OrderItem>(c.Items);
        CartListview.DataContext = coll;
        totalPrice.DataContext = c.TotalPrice;
    }

    /// <summary>
    /// get the items and the detailes from the customer and make an order
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MakeAnOrder(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.cart.MakeAnOrder(c, (Name.Text), (Email.Text), (Address.Text));
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// remove item from the cart
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    
    private void Remove(object sender, RoutedEventArgs e)
    {
        try
        {
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            c=bl.cart.UpdateQuantity(c, changed.ProductID, 0);
            coll.Remove(changed);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

/// <summary>
/// increase the amount of an item
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    private void Increase(object sender, RoutedEventArgs e)
    {
        try
        {
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            c=bl.cart.UpdateQuantity(c, changed.ProductID, changed.Amount + 1);
            coll = new();
            c.Items.Select(item =>
            {
                coll.Add(item);
                return item;
            }).ToList();
            CartListview.DataContext = coll;
            totalPrice.DataContext = c.TotalPrice;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    /// decrease the amount of an item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Decrease(object sender, RoutedEventArgs e)
    {
        try
        {
            OrderItem changed = (OrderItem)((Button)sender).DataContext;
            c=bl.cart.UpdateQuantity(c, changed.ProductID, changed.Amount - 1);
            coll = new();
            c.Items.Select(item =>
            {
                coll.Add(item);
                return item;
            }).ToList();
            CartListview.DataContext = coll;
            totalPrice.DataContext = c.TotalPrice;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}

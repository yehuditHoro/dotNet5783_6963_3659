using BlApi;
using BO;
using PL.PO;
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
    private PO.Cart PoCart=new();
    private BO.Cart BoCart=new();

    private ObservableCollection<BO.OrderItem> coll { get; set; }
    Tuple<Array,ObservableCollection<BO.OrderItem>,PO.Cart> tupleContext { get; set; }
    public CartWindow(IBl BL, BO.Cart cart)
    {
        InitializeComponent();
        bl = BL;
        PoCart = Converts.ConvertToPoCart(cart, PoCart);
        coll = new ObservableCollection<BO.OrderItem>(PoCart.Items);
        tupleContext = new Tuple<Array, ObservableCollection<OrderItem>,PO.Cart>(BO.eCategory.GetValues(typeof(BO.eCategory)), coll,PoCart);
        this.DataContext = tupleContext;
        CartListview.DataContext =coll;
        totalPrice.DataContext = PoCart.TotalPrice;
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
            BoCart = Converts.ConvertToBoCart(PoCart);
            bl.cart.MakeAnOrder(BoCart, (Name.Text), (Email.Text), (Address.Text));
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
            BoCart = Converts.ConvertToBoCart(PoCart);
            BoCart= bl.cart.UpdateQuantity(BoCart, changed.ProductID, 0);
            PoCart = Converts.ConvertToPoCart(BoCart, PoCart);
            coll.Remove(changed);
            CartListview.DataContext =coll;
            totalPrice.DataContext = PoCart.TotalPrice;

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
            BoCart=Converts.ConvertToBoCart(PoCart);
            BoCart=bl.cart.UpdateQuantity(BoCart, changed.ProductID, changed.Amount + 1);
            PoCart = Converts.ConvertToPoCart(BoCart, PoCart);
            coll = new();
            PoCart.Items.Select(item =>
            {
                coll.Add(item);
                return item;
            }).ToList();
            CartListview.DataContext = coll;
            totalPrice.DataContext = PoCart.TotalPrice;
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
            BoCart= Converts.ConvertToBoCart(PoCart);
            BoCart = bl.cart.UpdateQuantity(BoCart, changed.ProductID, changed.Amount - 1);
            PoCart = Converts.ConvertToPoCart(BoCart, PoCart);
            coll = new();
            PoCart.Items.Select(item =>
            {
                coll.Add(item);
                return item;
            }).ToList();
            CartListview.DataContext = coll;
            totalPrice.DataContext = PoCart.TotalPrice;

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

}

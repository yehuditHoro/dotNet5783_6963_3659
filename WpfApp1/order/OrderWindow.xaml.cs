using BlApi;
using BlImplementation;
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

namespace PL;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    private IBl bl;
    private bool isInitilize = false;
    private Window last;
    private ObservableCollection<BO.OrderForList?> orderList;
    //BO.OrderForList orderForList = new();
    BO.Order order = new();
    bool IsCustomer;
    Tuple<Order, bool> tupleContext;
    public OrderWindow(IBl BL, string user, Window window, ObservableCollection<BO.OrderForList?> o = null, int? oId = null)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            last = window;
            orderList = o;
            status.ItemsSource = BO.eOrderStatus.GetValues(typeof(BO.eOrderStatus));
            order = bl.order.GetOrder((int)oId);
            if (user == "customer")
                IsCustomer = false;
            else {
                IsCustomer = true;
                
            }
            tupleContext = new Tuple<Order, bool>(order, IsCustomer);
            DataContext = tupleContext;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
    /// <summary>
    ///  change the status of an order 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void changeStatus(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            order.Status = (BO.eOrderStatus)status.SelectedItem;
            BO.OrderForList? ofl = orderList?.Where(o => o?.ID == order.ID).FirstOrDefault();
            int? idx = -1;
            if (ofl != null)
            {
                idx = orderList?.IndexOf(ofl);
                orderList?.Remove(orderList?.Where(o => o?.ID == order.ID).FirstOrDefault());
            }
            if (order.Status == (BO.eOrderStatus)1)
            {
                order = bl.order.ShipedOrder(order.ID);
                ofl.Status = BO.eOrderStatus.shiped;
            }
            if (order.Status == (BO.eOrderStatus)2)
            {
                order = bl.order.DeliveredOrder(order.ID);
                ofl.Status = BO.eOrderStatus.delivered;
            }
            isInitilize = false;
            orderList?.Insert(idx ?? -1, ofl);
            last.Show();
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
        //try
        //{
        //    OrderItem changed = (OrderItem)((Button)sender).DataContext;
        //    c = bl.cart.UpdateQuantity(c, changed.ProductID, 0);
        //    coll.Remove(changed);
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}
    }

    /// <summary>
    /// increase the amount of an item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Increase(object sender, RoutedEventArgs e)
    {
        //try
        //{
        //    OrderItem changed = (OrderItem)((Button)sender).DataContext;
        //    c = bl.cart.UpdateQuantity(c, changed.ProductID, changed.Amount + 1);
        //    coll = new();
        //    c.Items.Select(item =>
        //    {
        //        coll.Add(item);
        //        return item;
        //    }).ToList();
        //    CartListview.DataContext = coll;
        //    totalPrice.DataContext = c.TotalPrice;
        //}
        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.Message);
        //}
    }
    /// <summary>
    /// decrease the amount of an item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Decrease(object sender, RoutedEventArgs e)
    {
    /*    try
        {
            bl.order.UpdateOrder

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }*/
    }
}


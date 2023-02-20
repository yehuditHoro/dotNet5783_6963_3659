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
    BO.OrderForList orderForList = new();
    BO.Order order = new();
    bool IsCustomer;
    Tuple<OrderForList, bool> tc;
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
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.Status = order.Status;
            orderForList.AmountOfItems = order.Items.Count();
            orderForList.TotalPrice = order.TotalPrice;
            if (user == "customer")
                //status.Visibility = Visibility.Hidden;
                IsCustomer = false;
            else IsCustomer = true;
            //CustomerStatus.Visibility = Visibility.Hidden;
            tc = new Tuple<OrderForList, bool>(orderForList, IsCustomer);
            DataContext = tc;
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

            //DataContext = orderForList;
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
                order = bl.order.ShipedOrder(orderForList.ID);
                ofl.Status = BO.eOrderStatus.shiped;
            }
            if (order.Status == (BO.eOrderStatus)2)
            {
                order = bl.order.DeliveredOrder(orderForList.ID);
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
}


using BlApi;
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

namespace PL;

/// <summary>
/// Interaction logic for OrderWindow.xaml
/// </summary>
public partial class OrderWindow : Window
{
    private IBl bl;
    private bool isInitilize = false;
    BO.OrderForList orderForList = new();
    BO.Order order = new();
    public OrderWindow(IBl BL, string user, int? oId = null)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            status.ItemsSource = BO.Enums.eOrderStatus.GetValues(typeof(BO.Enums.eOrderStatus));
            order = bl.order.GetOrder((int)oId);
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.Status = order.Status;
            orderForList.AmountOfItems = order.Items.Count();
            orderForList.TotalPrice = order.TotalPrice;
            DataContext = orderForList;
            if (user == "customer")
                status.Visibility = Visibility.Hidden;
            else CustomerStatus.Visibility = Visibility.Hidden;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void changeStatus(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (isInitilize)
            {
                DataContext = orderForList;
                order.Status = (BO.Enums.eOrderStatus)status.SelectedItem;
                if (order.Status == (BO.Enums.eOrderStatus)1)
                    order = bl.order.ShipedOrder(orderForList.ID);
                if (order.Status == (BO.Enums.eOrderStatus)2)
                    order = bl.order.DeliveredOrder(orderForList.ID);
                OrderListWindow window = new(bl);
                window.Show();
                this.Close();
            }
            else isInitilize = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}


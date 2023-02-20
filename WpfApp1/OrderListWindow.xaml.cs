using BlApi;
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
/// Interaction logic for OrderListWindow.xaml
/// </summary>
public partial class OrderListWindow : Window
{
    private IBl bl;
    private ObservableCollection<BO.OrderForList?> orderList { get; set; }

    public OrderListWindow(IBl BL)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            orderList = new ObservableCollection<BO.OrderForList?>(bl.order.GetOrdersList());
            DataContext = orderList;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void GetOrder(object sender, MouseButtonEventArgs e)
    {
        new OrderWindow(bl, "manager", this, orderList, ((BO.OrderForList)OrdersView.SelectedItem).ID).Show();
    }
}


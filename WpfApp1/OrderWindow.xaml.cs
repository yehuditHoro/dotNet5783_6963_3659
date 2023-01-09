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

namespace PL
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        private IBl bl;
        private bool isInitilize = false;
        public OrderWindow(IBl BL, int? oId = null)
        {
            InitializeComponent();
            bl = BL;
            status.ItemsSource = BO.Enums.eOrderStatus.GetValues(typeof(BO.Enums.eOrderStatus));
            BO.OrderForList orderForList = new();
            BO.Order order = new();
            order = bl.order.GetOrder((int)oId);
            orderForList.ID = order.ID;
            orderForList.CustomerName = order.CustomerName;
            orderForList.Status = order.Status;
            orderForList.AmountOfItems = order.Items.Count();
            orderForList.TotalPrice = order.TotalPrice;
            DataContext = orderForList;
        }

        private void changeStatus(object sender, SelectionChangedEventArgs e)
        {
           // if()
        }
    }
}

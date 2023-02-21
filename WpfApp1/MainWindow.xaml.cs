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
/// Interaction logic for Window1.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl = BlApi.Factory.Get();
    public BO.Cart cart = new();
    public MainWindow()
    {
        InitializeComponent();
        int? id=bl.order.ChooseOrder();
    }

    /// <summary>
    /// move the admin to his page and show hin his options
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AdminScreen(object sender, RoutedEventArgs e)
    {
        AdminWindow adminWindow = new(bl);
        adminWindow.Show();
    }
    /// <summary>
    /// gets number of orde and track its status
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Tracking(object sender, RoutedEventArgs e)
    {
        try { 
        OrderTrackingWindow tracking = new(bl, Convert.ToInt32(orderId.Text));
        tracking.Show();
        }
        catch
        {
            MessageBox.Show("No order number entered");
        }
    }
    /// <summary>
    /// move the customer to hus screen - new order window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void CustomerScreen(object sender, RoutedEventArgs e)
    {
        CatalogWindow catalogWindow = new(bl, cart);
        catalogWindow.Show();
    }

    private void Simulation(object sender, RoutedEventArgs e)
    {
        SimulatorWindow simulator = new();
        simulator.Show();
    }
}


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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MainWindow;
using BlImplementation;
using BlApi;
namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private IBl bl = new Bl();
    public MainWindow()
    {
        InitializeComponent();
    }
    /// <summary>
    /// move the user to the product window in order to see all the products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void GetProducts(object sender, RoutedEventArgs e)
    {
        ProductListWindow ListWindow = new ProductListWindow(bl);
        ListWindow.Show();
        this.Hide();
    }
}

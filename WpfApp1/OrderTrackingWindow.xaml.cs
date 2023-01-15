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
/// Interaction logic for OrderTrackingWindow.xaml
/// </summary>
public partial class OrderTrackingWindow : Window
{
    private IBl bl;
    private int oID;
    public OrderTrackingWindow(IBl BL, int id)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            oID = id;
            BO.OrderTracking current = bl.order.OrderTrack(id);
            DataContext = current; //איך אפשר להציג את הנתונים מתוך הרשימה מסוג טאפל?
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }

    private void OrderDetails(object sender, RoutedEventArgs e)
    {
        OrderWindow order = new(bl, "customer", oID);
        order.Show();
        this.Close();
    }
}


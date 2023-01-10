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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBl bl = BlApi.Factory.Get();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AdminScreen(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new(bl);
            adminWindow.Show();
            this.Close();
        }

        private void Tracking(object sender, RoutedEventArgs e)
        {

        }

        private void CustomerScreen(object sender, RoutedEventArgs e)
        {
            CatalogWindow catalogWindow = new(bl);
            catalogWindow.Show();
            this.Close();
        }
    }
}

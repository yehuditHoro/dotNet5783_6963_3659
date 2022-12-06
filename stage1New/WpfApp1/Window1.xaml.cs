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
using BlApi;
namespace MainWindow
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private IBl bl;
        public Window1(IBl BL)
        {
            InitializeComponent();
            bl = BL;
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string name=(sender as Button).Content.ToString();
            string k = name;
        }
    }
}

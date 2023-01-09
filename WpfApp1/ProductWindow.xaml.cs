using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using PL;

namespace MainWindow;

/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl;
    private int p_id;
    //private Window window;
    BO.Product product = new();
    public ProductWindow(IBl BL, Window w,int? pId = null)
    {
        //window = w;
        try
        {
            InitializeComponent();
            bl = BL;
            category.ItemsSource = BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
            if (pId == null)
            {
                p_id = -1;
                addOrUpdate.Content = "add";
                btnDelete.Visibility = Visibility.Hidden;
            }
            else
            {
                addOrUpdate.Content = "update";
                p_id = (int)pId;
                product = bl.product.GetProductItemsForManager(p_id);
            }
            DataContext = product;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// add or update the product due to the request
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Submit(object sender, RoutedEventArgs e)
    {
        try
        {
            if (p_id == -1)
            {
                bl.product.AddProduct(product);
            }
            else
            {
                bl.product.UpdateProduct(product);
            }
            ProductListWindow wi = new ProductListWindow(bl);
            wi.Show();
            this.Close();
            //window.Show();
            //this.Hide();
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }

    }
    
    /// <summary>
    /// delete the product
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Delete(object sender, RoutedEventArgs e)
    {
        try
        {
            bl.product.RemoveProduct(p_id);
            ProductListWindow wi = new ProductListWindow(bl);
            wi.Show();
            this.Close();
        }
        catch (Exception ex)

        { MessageBox.Show(ex.Message); }
    }
}

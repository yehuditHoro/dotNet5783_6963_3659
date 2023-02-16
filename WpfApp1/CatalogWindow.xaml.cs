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
/// Interaction logic for CatalogWindow.xaml
/// </summary>
public partial class CatalogWindow : Window
{
    private IBl bl;
    private BO.Cart c;
    private ObservableCollection<BO.ProductItem> p { get; set; }

    public CatalogWindow(IBl BL, BO.Cart cart)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            c = cart;
            CategorySelector.ItemsSource = BO.eCategory.GetValues(typeof(BO.eCategory));
            IEnumerable <ProductItem?> productItems= bl.product.GetCatalog();
            p = new ObservableCollection<BO.ProductItem?>(productItems);
            CatalogListview.DataContext = productItems;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void GetCart(object sender, RoutedEventArgs e)
    {
        CartWindow cartWindow = new(bl, c);
        cartWindow.Show();
        this.Hide();
    }

    private void AddProductToCart(object sender, MouseButtonEventArgs e)
    {
        new ProductWindow(bl, "customer",this, null, c, ((BO.ProductItem)CatalogListview.SelectedItem).ID).Show();
        this.Hide();
    }

    private void Categories(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            CatalogListview.ItemsSource = bl.product.GetCatalog((BO.eCategory)CategorySelector.SelectedItem);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void DeleteFilter(object sender, RoutedEventArgs e)
    {
        try
        {
            CatalogListview.ItemsSource = bl.product.GetCatalog();
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }
}

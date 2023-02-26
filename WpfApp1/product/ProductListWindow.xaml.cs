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
using BO;
using BlApi;
using System.Collections.ObjectModel;

namespace PL;

/// <summary>
/// Interaction logic for Window2.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl;
    private ObservableCollection<ProductForList?> productList { get; set; }
    Tuple<Array, ObservableCollection<ProductForList?>> tupleContext;
    public ProductListWindow(IBl BL)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            //ComboBoxCategories.ItemsSource = BO.eCategory.GetValues(typeof(BO.eCategory));
            productList = new ObservableCollection<ProductForList?>(bl.product.GetProducts());
            //ProductsListview.DataContext = productList;
            tupleContext = new Tuple<Array, ObservableCollection<ProductForList?>>(BO.eCategory.GetValues(typeof(BO.eCategory)), productList);
            this.DataContext= tupleContext;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// show all the categories
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowCategories(object sender, SelectionChangedEventArgs e)
    {
        try
        {  
            productList = new ObservableCollection<BO.ProductForList?> ();
            ObservableCollection<BO.ProductForList?> t = new ObservableCollection<BO.ProductForList?>(bl.product.GetProducts());
            BO.eCategory category = (BO.eCategory)ComboBoxCategories.SelectedItem;
            var tmp = from product in t
                      group product by product.Category into newGroup
                      where newGroup.Key == category
                      select newGroup.ToList();
            this.productList = Converts.ConvertToProductList(tmp, productList);
            tupleContext = new Tuple<Array, ObservableCollection<ProductForList?>>(BO.eCategory.GetValues(typeof(BO.eCategory)), productList);
            this.DataContext = tupleContext;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    /// <summary>
    /// move the user to the add product window(product window)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProduct(object sender, RoutedEventArgs e)
    {
        ProductWindow window = new ProductWindow(bl, "manager", this, productList);
        window.Show();
    }

    /// <summary>
    /// delete the filter and show all the products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteFilter_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            productList = new ObservableCollection<ProductForList?>(bl.product.GetProducts());
            tupleContext = new Tuple<Array, ObservableCollection<ProductForList?>>(BO.eCategory.GetValues(typeof(BO.eCategory)), productList);
            this.DataContext = tupleContext;
            ProductsListview.DataContext = productList;
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }

    /// <summary>
    /// move the user to the update product window(product window)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Update(object sender, MouseButtonEventArgs e)
    {
        ProductWindow productWindow = new(bl, "manager", this, productList, null, ((BO.ProductForList)ProductsListview.SelectedItem).ID);
        productWindow.Show();
    }

}

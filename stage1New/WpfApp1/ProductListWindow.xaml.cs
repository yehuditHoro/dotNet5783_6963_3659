﻿using System;
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
using MainWindow;
namespace PL;

/// <summary>
/// Interaction logic for Window2.xaml
/// </summary>
public partial class ProductListWindow : Window
{
    private IBl bl;

    public ProductListWindow(IBl BL)
    {
        InitializeComponent();
        bl = BL;
        ProductsListview.ItemsSource = bl.product.GetProducts();
        ComboBoxSelector.ItemsSource =  BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
    }
    /// <summary>
    /// show all the categories
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ShowCategories(object sender, SelectionChangedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.product.GetProducts((Enums.eCategory)ComboBoxSelector.SelectedItem);
    }
    /// <summary>
    /// move the user to the add product window(product window)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AddProduct(object sender, RoutedEventArgs e)
    {
        ProductWindow window = new ProductWindow(bl);
        window.Show();
        this.Hide();
    }
    /// <summary>
    /// delete the filter and show all the products
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void DeleteFilter_Click(object sender, RoutedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.product.GetProducts();

    }
    /// <summary>
    /// move the user to the update product window(product window)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Update(object sender, MouseButtonEventArgs e)
    {
        new ProductWindow(bl, ((BO.ProductForList)ProductsListview.SelectedItem).ID).Show();
        Close();
    }

 
}

﻿using BlApi;
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
    //public Tuple<Array, IEnumerable<ProductItem?>> tuple;
    //IEnumerable<ProductItem?> pi;
    //Array arr;
    public CatalogWindow(IBl BL, BO.Cart cart)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            c = cart;
            //arr = BO.eCategory.GetValues(typeof(BO.eCategory));
            //pi = bl.product.GetCatalog();
            //tuple = new Tuple<Array, IEnumerable<ProductItem?>>(arr, pi);
            //this.DataContext = tuple;
            CategorySelector.ItemsSource = BO.eCategory.GetValues(typeof(BO.eCategory));
            CatalogListview.ItemsSource = bl.product.GetCatalog();
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
        //this.Hide();
    }

    private void AddProductToCart(object sender, MouseButtonEventArgs e)
    {
        new ProductWindow(bl, "customer",this, null, c, ((BO.ProductItem)CatalogListview.SelectedItem).ID).Show();
    }

    private void Categories(object sender, SelectionChangedEventArgs e)
    {
        try
        {
           //tuple.Item2 = bl.product.GetCatalog((BO.eCategory)CategorySelector.SelectedItem);
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
            //tuple.Item2 = bl.product.GetCatalog();
            CatalogListview.ItemsSource = bl.product.GetCatalog();
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }
}

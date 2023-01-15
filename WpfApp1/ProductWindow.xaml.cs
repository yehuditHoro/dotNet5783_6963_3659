﻿using System;
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
using BO;

namespace PL;
/// <summary>
/// Interaction logic for Window1.xaml
/// </summary>
public partial class ProductWindow : Window
{
    private IBl bl;
    private BO.Cart? c;
    BO.Product product = new();
    public ProductWindow(IBl BL, string userType ,BO.Cart? cart=null ,int? pId = null)
    {
        try
        {
            InitializeComponent();
            bl = BL;
            c = cart;
            category.ItemsSource = BO.eCategory.GetValues(typeof(BO.eCategory));
            if (userType == "manager")
            {
                if (pId == null)
                {
                    addOrUpdate.Content = "add";
                    btnDelete.Visibility = Visibility.Hidden;
                }
                else
                {
                    addOrUpdate.Content = "update";
                    product = bl.product.GetProductItemsForManager((int)pId);
                }
                DataContext = product;
            }
            else
            {
                addOrUpdate.Content = "add to cart";
                btnDelete.Visibility = Visibility.Hidden;
                product = bl.product.GetProductItemsForManager((int)pId);  //cart??????????
                //BO.ProductItem productItem = new();
                //product.Name = productItem.Name;
                //product.Category = productItem.Category;
                //product.Price = productItem.Price;
                //product.InStock = productItem.Amount;
                DataContext = product;
            }
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
            if (addOrUpdate.Content == "add")
            {
                bl.product.AddProduct(product);
                ProductListWindow wi = new ProductListWindow(bl);
                wi.Show();
                this.Close();
            }
            else if (addOrUpdate.Content == "add to cart")
            {
                bl.cart.addToCart(c, product.ID);
                CatalogWindow catalog = new(bl, c);
                catalog.Show();
                this.Close();
            }
            else
            {
                bl.product.UpdateProduct(product);
                ProductListWindow wi = new ProductListWindow(bl);
                wi.Show();
                this.Close();
            }          
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
            bl.product.RemoveProduct(product.ID);
            ProductListWindow wi = new ProductListWindow(bl);
            wi.Show();
            this.Close();
        }
        catch (Exception ex)
        { MessageBox.Show(ex.Message); }
    }
}

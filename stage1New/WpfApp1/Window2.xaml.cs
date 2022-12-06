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
namespace PL;

/// <summary>
/// Interaction logic for Window2.xaml
/// </summary>
public partial class Window2 : Window
{
    private IBl bl;

    public Window2(IBl BL)
    {
        InitializeComponent();
        bl = BL;
        ProductsListview.ItemsSource = bl.product.GetProducts();
        ComboBoxSelector.ItemsSource = BO.Enums.eCategory.GetValues(typeof(BO.Enums.eCategory));
    }

    private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }

    private void ComboBoxSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ProductsListview.ItemsSource = bl.product.GetByFilter((x => x.Category == (Enums.eCategory)ComboBoxSelector.SelectedItem));
        //List<Enums.eCategory> o = new();
        //foreach (Enums.eCategory i in Enums.eCategory.GetValues(typeof(BO.Enums.eCategory)))
        //{
        //    if (i != (Enums.eCategory)ComboBoxSelector.SelectedItem)
        //        o.Add(i);
        //}
        //ComboBoxSelector.ItemsSource = o;
    }

}

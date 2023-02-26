using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BlApi;
using BO;
using static NPOI.HSSF.Util.HSSFColor;

namespace PL.PO
{
    public class Cart : DependencyObject
    {
        BlApi.IBl bl;
        public string CustomerName
        {
            get { return (string)GetValue(CustomerNameProperty); }
            set { SetValue(CustomerNameProperty, value); }
        }
        public static readonly DependencyProperty CustomerNameProperty = DependencyProperty.Register("CustomerName", typeof(string), typeof(Cart), new UIPropertyMetadata(null));
        public string CustomerEmail
        {
            get { return (string)GetValue(CustomerEmailProperty); }
            set { SetValue(CustomerEmailProperty, value); }
        }
        public static readonly DependencyProperty CustomerEmailProperty = DependencyProperty.Register("CustomerEmail", typeof(string), typeof(Cart), new UIPropertyMetadata(null));
        public string CustomerAddress
        {
            get { return (string)GetValue(CustomerAddressProperty); }
            set { SetValue(CustomerAddressProperty, value); }
        }
        public static readonly DependencyProperty CustomerAddressProperty = DependencyProperty.Register("CustomerAddress", typeof(string), typeof(Cart), new UIPropertyMetadata(null));
        public double TotalPrice
        {
            get { return (double)GetValue(TotalPriceProperty); }
            set { SetValue(TotalPriceProperty, value); }
        }
        public static readonly DependencyProperty TotalPriceProperty = DependencyProperty.Register("TotalPrice", typeof(double), typeof(Cart), new UIPropertyMetadata((double)0));

        public List<OrderItem> Items
        {
            get { return (List<OrderItem>)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }
        public static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(List<OrderItem>), typeof(Cart), new UIPropertyMetadata(new List<OrderItem>()));

    }
}
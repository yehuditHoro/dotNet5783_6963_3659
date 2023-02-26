using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public static class Converts
    {


        public static ObservableCollection<ProductForList?> ConvertToProductList(IEnumerable<List<BO.ProductForList?>> Blist, ObservableCollection<ProductForList?> p)
        {
            //ObservableCollection<ProductForList?> Plist = new();
            //p.Clear();

            foreach (var group in Blist)
            {
                foreach (var item in group)
                {
                    p.Add(item);
                }
            }
            return p;
        }

        public static BO.Cart ConvertToBoCart(PO.Cart Bp)
        {
            BO.Cart item = new()
            {
                CustomerAddress = Bp.CustomerAddress,
                CustomerEmail = Bp.CustomerEmail,
                CustomerName = Bp.CustomerName,
                Items = (Bp.Items),
                TotalPrice = Bp.TotalPrice,
            };
            return item;
        }

        public static PO.Cart ConvertToPoCart(BO.Cart Bc, PO.Cart Pc)
        {

            Pc.CustomerAddress = Bc.CustomerAddress;
            Pc.CustomerEmail = Bc.CustomerEmail;
            Pc.CustomerName = Bc.CustomerName;
            Pc.Items = (Bc.Items);
            Pc.TotalPrice = Bc.TotalPrice;

            return Pc;
        }

        public static ProductForList ConvertProductForListToProduct(Product product)
        {
            ProductForList p = new();
            p.ID = product.ID;
            p.Name = product.Name;
            p.Price = product.Price;
            p.Category = (eCategory)product.Category;
            return p;
        }
    }
}

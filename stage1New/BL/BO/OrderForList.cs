using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class OrderForList
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public BO.Enums.OrderStatus Status { get; set; }   
        public int AmountOfItems { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString() => $@"
       ID: {ID}, 
       Customer name: {CustomerName},
       Status: {Status},
       Amount Of Items {AmountOfItems},
       Total price: {TotalPrice}";

    }
}

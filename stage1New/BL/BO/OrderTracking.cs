using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    internal class OrderTracking
    {
        public int ID { get; set; }
        public BO.Enums.OrderStatus Status { get; set; }
    }
}

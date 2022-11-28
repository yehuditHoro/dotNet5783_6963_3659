using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

namespace BlImplementation
{
    sealed public class Bl : IBl
    {
        public Iproduct product => new BlProduct();

        public Iorder order => new BlOrder();

        public Icart cart => new BlCart();
    }
}

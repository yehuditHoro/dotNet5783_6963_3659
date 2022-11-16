using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IDal
    {
        public Iorder order { get; }
        public IorderItem orderItem { get; }
        public Iproduct product { get; }

    }
}

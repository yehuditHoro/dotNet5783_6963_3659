using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DalApi;
using Dal.DO;
namespace Dal;
sealed internal class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    public Iorder order => new DalOrder();
    public IorderItem orderItem => new DalOrderItem();
    public Iproduct product => new DalProduct();
}

using BlApi;
using DalApi;
using dalList;
namespace BlImplementation;

internal class BlOrder : BlApi.Iorder
{
    IDal Dal = new DalList(); 
    public BO.Order DeliveredOrder(int id)
    {
        throw new NotImplementedException();
    }

    public BO.Order GetOrderItem(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<BO.OrderForList> GetOrdersList(BO.OrderForList ordersList)
    {
        throw new NotImplementedException();
    }

    public BO.Order ShipedOrder(int id)
    {
        throw new NotImplementedException();
    }
}


using BlApi;
using DalApi;
using dalList;
namespace BlImplementation;

internal class BlOrder : BlApi.Iorder
{
    IDal Dal = new DalList();
    public IEnumerable<BO.OrderForList>GetOrdersList()
    {
        IEnumerable<Dal.DO.Order> getOrders = Dal.order.ReadAll();
        if (getOrders.Count() <= 0)
        {
            throw new Exception();
        }
        List<BO.OrderForList> boOrders = new();
        foreach (Dal.DO.Order p in getOrders)
        {
            BO.ProductForList bp = new BO.ProductForList();
            bp.ID = p.ID;
            bp.Name = p.Name;
            bp.Price = p.Price;
            bp.Category = (BO.Enums.eCategory)p.Category;
            //bp.InStock = p.InStock;
            boProducts.Add(bp);
        }
        return boProducts;
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


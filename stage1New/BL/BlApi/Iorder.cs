using BO;
namespace BlApi;

public interface Iorder
{
    public IEnumerable<OrderForList> GetOrdersList(OrderForList ordersList);
    public Order GetOrderItem(int id);
    public Order ShipedOrder(int id);
    public Order DeliveredOrder(int id);
    
    //public void UpdateOrder(int id); //bonus
}

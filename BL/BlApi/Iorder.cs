using BO;
namespace BlApi;

public interface Iorder
{
    public IEnumerable<OrderForList> GetOrdersList();
    public Order GetOrder(int id);
    public Order ShipedOrder(int id);
    public Order DeliveredOrder(int id);
    public OrderTracking OrderTrack(int id);
    public int? ChooseOrder();
    
    //public void UpdateOrder(int id); //bonus
   
}

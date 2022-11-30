
using Dal.DO;
namespace dalList;
using System.Collections.Generic;
using DalApi;
public class DalOrderItem : IorderItem
{
    /// <summary>
    /// add a new order item to the order item list
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <returns></returns>
    /// <exception cref="EntityDuplicateException"></exception>
    public int Add(OrderItem newOrderItem)
    {
        newOrderItem.ID = DataSource.config.OrderItemId;
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == newOrderItem.ID)
            {
                throw new EntityDuplicateException("this order item already exist");
            }
        }
        DataSource.OrderItemsList.Add(newOrderItem);
        return newOrderItem.ID;
    }
    /// <summary>
    /// get the specific order item with the specific id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public OrderItem Read(int id)
    {
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                return DataSource.OrderItemsList[i];
            }
        }
        throw new EntityNotFoundException("this id doesn't exist");
    }
    /// <summary>
    ///  returns all the order items in the order items list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> ReadAll()
    {
        List <OrderItem> allOrderItems = new List<OrderItem>();
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            allOrderItems.Add(DataSource.OrderItemsList[i]);
        }
        return allOrderItems;
    }
    /// <summary>
    /// update the order item in the order item list
    /// </summary>
    /// <param name="newOrderItem"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Update(OrderItem newOrderItem)
    {
        int id = newOrderItem.ID;
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList[i] = newOrderItem;
                return;
            }
        }
        throw new EntityNotFoundException("this order item doesn't exist");
    }
    /// <summary>
    /// gets id and delete the specific item of the order item list
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="EntityNotFoundException"></exception>
    public void Delete(int id)
    {
        for (int i = 0; i < DataSource.OrderItemsList.Count(); i++)
        {
            if (DataSource.OrderItemsList[i].ID == id)
            {
                DataSource.OrderItemsList.Remove(DataSource.OrderItemsList[i]);
                return;
            }
        }
        throw new EntityNotFoundException("this order item doesn't exist");
    }
}

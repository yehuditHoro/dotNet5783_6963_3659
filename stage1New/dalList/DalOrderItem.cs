
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
        return DataSource.OrderItemsList.Find(OI => OI.ID == id);
        throw new EntityNotFoundException("this id doesn't exist");
    }
    /// <summary>
    ///  returns all the order items in the order items list
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderItem> ReadAll(Func<OrderItem, bool>? func = null)
    {
        try
        { 
            List<OrderItem> allOrderItems = new (DataSource.OrderItemsList);
            return func == null ? allOrderItems : allOrderItems.Where(func);
        }
        catch (DalApi.EntityNotFoundException)
        {
            throw new EntityNotFoundException("entity not found");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public OrderItem ReadSingle(Func<OrderItem, bool> func)
    {
        return DataSource.OrderItemsList.Where(func).ToList()[0];
        throw new EntityNotFoundException("order item not found");
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
